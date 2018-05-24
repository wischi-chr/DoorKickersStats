using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DoorKickersWeaponStat.HotFixXml;

namespace DoorKickersWeaponStat
{
    public static class XmlFirearmLoader
    {
        public static IEnumerable<Firearm> LoadAllFirearmsFromGameLocation(string doorKickersGamePath)
        {
            var dataLocation = Path.Combine(doorKickersGamePath, "data", "object_library");
            var files = Directory.GetFiles(dataLocation, "*equipment*.xml");

            return ParseFirearmsFromFiles(files);
        }

        public static IEnumerable<Firearm> ParseFirearmsFromFiles(params string[] fileNames)
        {
            foreach (var file in fileNames)
            {
                var fName = Path.GetFileName(file);
                var content = File.ReadAllText(file);
                content = DefaultContentFixes.ApplyFixForFile(fName, content);

                var doc = XDocument.Parse(content);
                foreach (var firearm in ParseFirearms(doc))
                    yield return firearm;
            }
        }

        public static IEnumerable<Firearm> ParseFirearms(XDocument doc)
        {
            if (doc.Root.Name != "Equipment")
                yield break;

            foreach (var e in ParseFirearmsFromEquipmentElement(doc.Root))
                yield return e;
        }

        public static IEnumerable<Firearm> ParseFirearmsFromEquipmentElement(XElement equipmentElement)
        {
            return equipmentElement.Elements("Firearm").Select(ParseFirearmFromXmlElement).Where(f => f != null);
        }

        //This are not real fireams but dummy entries to calculate percentages for the GUI
        public static string[] dummyFirearmStats = new string[] {
            "pistolWorstStats", "pistolBestStats",
            "tazerWorstStats", "tazerBestStats",
            "rifleWorstStats", "rifleBestStats",
            "shotgunWorstStats", "shotgunBestStats"
        };

        public static Firearm ParseFirearmFromXmlElement(XElement firearm)
        {
            try
            {
                var modParams = firearm.Element("ModifiableParams");
                var mobModifiers = firearm.Element("MobilityModifiers");
                var name = firearm.Attribute("name").Value;

                if (dummyFirearmStats.Contains(name))
                    return null;

                return new Firearm(
                    name,
                    ParseWeaponCategory(firearm.Attribute("category").Value),
                    ParseWeaponClass(firearm),
                    ParseInventorySlot(firearm.Attribute("inventoryBinding")?.Value),
                    Convert.To<int?>(firearm.Attribute("unlockCost")?.Value) ?? 0,
                    int.Parse(modParams.Attribute("roundsPerMagazine").Value),
                    decimal.Parse(modParams.Attribute("roundsPerSecond").Value, CultureInfo.InvariantCulture),
                    int.Parse(modParams.Attribute("damagePerBullet").Value),
                    int.Parse(modParams.Attribute("armorPiercingLevel").Value),
                    decimal.Parse(modParams.Attribute("muzzleDropDistanceMeters").Value, CultureInfo.InvariantCulture),
                    Convert.To<int?>(mobModifiers?.Attribute("moveSpeedLocalModifierPercent")?.Value) ?? 0,
                    Convert.To<int?>(mobModifiers?.Attribute("turnSpeedLocalModifierPercent")?.Value) ?? 0,
                    int.Parse(modParams.Attribute("numPellets").Value),
                    decimal.Parse(modParams.Attribute("spreadAt10Meters").Value, CultureInfo.InvariantCulture),
                    int.Parse(modParams.Attribute("silenced").Value) == 1,
                    decimal.Parse(modParams.Attribute("shotSoundMeters").Value, CultureInfo.InvariantCulture),
                    int.Parse(modParams.Attribute("closedBolt").Value) == 1,
                    int.Parse(modParams.Attribute("cyclicReload").Value) == 1,
                    int.Parse(modParams.Attribute("reloadTime").Value),
                    int.Parse(modParams.Attribute("reloadEmptyTime").Value),
                    int.Parse(modParams.Attribute("changeInTime").Value),
                    int.Parse(modParams.Attribute("changeOutTime").Value),
                    int.Parse(modParams.Attribute("readyTime").Value),
                    int.Parse(modParams.Attribute("guardTime").Value)

                );
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
                Debugger.Break();
            }

            return null;
        }

        public static int? NullableInt(string val)
        {
            if (int.TryParse(val, out var res))
                return res;
            return null;
        }

        public static WeaponClass ParseWeaponClass(XElement firearmElement)
        {
            var c = WeaponClass.None;

            foreach (var classElement in firearmElement.Elements("Class"))
                c |= ParseWeaponClass(classElement.Attribute("value").Value);

            return c;
        }

        public static WeaponClass ParseWeaponClass(string weaponClassName)
        {
            if (string.IsNullOrWhiteSpace(weaponClassName))
                return WeaponClass.None;

            switch (weaponClassName)
            {
                case "AssaultEnemy": return WeaponClass.AssaultEnemy;
                case "Pointman": return WeaponClass.Pointman;
                case "Assaulter": return WeaponClass.Assaulter;
                case "Breacher": return WeaponClass.Breacher;
                case "Stealth": return WeaponClass.Stealth;
                case "Shield": return WeaponClass.Shield;
                case "PointmanEnemy": return WeaponClass.PointmanEnemy;
                default: Debugger.Break(); break;
            }

            throw new ArgumentException();
        }

        public static InventorySlot ParseInventorySlot(string slotName)
        {
            if (string.IsNullOrWhiteSpace(slotName))
                return InventorySlot.None;

            switch (slotName)
            {
                case "PrimaryWeapon": return InventorySlot.PrimaryWeapon;
                case "SecondaryWeapon": return InventorySlot.SecondaryWeapon;
                case "UtilityPouch": return InventorySlot.UtilityPouch;
                default: Debugger.Break(); break;
            }

            throw new ArgumentException();
        }

        public static WeaponCategory ParseWeaponCategory(string weaponCategory)
        {
            if (string.IsNullOrWhiteSpace(weaponCategory))
                return WeaponCategory.Unknown;

            switch (weaponCategory)
            {
                case "rifle": return WeaponCategory.Rifle;
                case "pistol": return WeaponCategory.Pistol;
                case "shotgun": return WeaponCategory.Shotgun;
                case "tazer": return WeaponCategory.Tazer;
                default: Debugger.Break(); break;
            }

            throw new ArgumentException();
        }
    }
}

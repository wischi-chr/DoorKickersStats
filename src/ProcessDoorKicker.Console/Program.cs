using DoorKickersWeaponStat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Sry no setting file: Change the settings here:
            var doorKickersPath = @"D:\SteamLibrary\steamapps\common\DoorKickers";
            var outputFile = @"..\..\..\..\data\info.tsv";

            var result = XmlFirearmLoader.LoadAllFirearmsFromGameLocation(doorKickersPath).ToList();

            using (var fileStream = File.Open(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(fileStream))
            {
                WriteTsv(writer, result);
            }
        }

        private static void WriteTsv(TextWriter writer, IEnumerable<Firearm> firearms)
        {
            WriteTsvHeader(writer);
            foreach (var f in firearms)
            {
                WriteFirearmTsvLine(writer, f);
            }
        }

        private static void WriteTsvHeader(TextWriter writer)
        {
            var headers = new string[]
            {
                "Name",
                "Category",
                "Unlock cost",
                "Rounds per magazine",
                "Rounds per second",
                "Damage per bullet",
                "Armor piercing level",
                "Muzzle drop distance meters",
                "Move speed local modifier percent",
                "Turn speed local modifier percent",
                "Num pellets",
                "Spread at 10 meters",
                "Silenced",
                "Shot sound meters",
                "Closed bolt",
                "Cyclic reload",
                "Reload time",
                "Reload empty time",
                "Change in time",
                "Change out time",
                "Ready time",
                "Guard time"
            };

            foreach (var h in headers)
            {
                writer.Write(h);
                writer.Write('\t');
            }

            writer.WriteLine();
        }

        private static void WriteFirearmTsvLine(TextWriter writer, Firearm f)
        {
            var formater = CultureInfo.InvariantCulture;
            var values = new string[]
            {
                f.Name,
                f.WeaponCategory.ToString(),
                f.UnlockCost.ToString(formater),
                f.RoundsPerMagazine.ToString(formater),
                f.RoundsPerSecond.ToString(formater),
                f.DamagePerBullet.ToString(formater),
                f.ArmorPiercingLevel.ToString(formater),
                f.MuzzleDropDistanceMeters.ToString(formater),
                f.MoveSpeedLocalModifierPercent.ToString(formater),
                f.TurnSpeedLocalModifierPercent.ToString(formater),
                f.NumPellets.ToString(formater),
                f.SpreadAt10Meters.ToString(formater),
                f.Silenced ? "1" : "0",
                f.ShotSoundMeters.ToString(formater),
                f.ClosedBolt ? "1" : "0",
                f.CyclicReload ? "1" : "0",
                f.ReloadTime.ToString(formater),
                f.ReloadEmptyTime.ToString(formater),
                f.ChangeInTime.ToString(formater),
                f.ChangeOutTime.ToString(formater),
                f.ReadyTime.ToString(formater),
                f.GuardTime.ToString(formater)
            };

            foreach (var v in values)
            {
                writer.Write(v);
                writer.Write('\t');
            }

            writer.WriteLine();
        }
    }
}

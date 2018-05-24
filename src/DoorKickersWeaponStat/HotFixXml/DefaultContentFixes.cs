using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorKickersWeaponStat.HotFixXml
{
    internal class DefaultContentFixes
    {
        private static void ApplyEquipmentXmlFix(StringBuilder content)
        {
            //This fix is needed because there is no space between the attributes in the original
            //equiment.xml

            content.Replace("inventoryBinding=\"Armor\"unlockCost=\"15\"", "inventoryBinding=\"Armor\" unlockCost=\"15\"");
        }

        private static void ApplyEquipmentPistolXmlFix(StringBuilder content)
        {
            //Fix the invalid characters inside the comments (double dash is not allowed inside xml comments)

            content.Replace(" -- ", " // ");
        }

        public static string ApplyFixForFile(string fileName, string content)
        {
            var sb = new StringBuilder(content);
            ApplyFixForFile(fileName, sb);
            return sb.ToString();
        }

        public static void ApplyFixForFile(string fileName, StringBuilder content)
        {
            switch (fileName)
            {
                case "equipment.xml":
                    ApplyEquipmentXmlFix(content);
                    break;

                case "equipment_pistols.xml":
                    ApplyEquipmentPistolXmlFix(content);
                    break;
            }
        }
    }
}

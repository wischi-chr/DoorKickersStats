using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorKickersWeaponStat
{
    public static class Convert
    {
        //Source: https://stackoverflow.com/questions/45030/how-to-parse-a-string-into-a-nullable-int
        public static T To<T>(object value)
        {
            try { return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value.ToString()); }
            catch { return default(T); }
        }
    }
}

using System;

namespace DoorKickersWeaponStat
{
    [Flags]
    public enum InventorySlot
    {
        None = 0,
        PrimaryWeapon = 1 << 0,
        SecondaryWeapon = 1 << 1,
        Armor = 1 << 2,
        UtilityPouch = 1 << 3,
        SupportGear = 1 << 4
    }
}

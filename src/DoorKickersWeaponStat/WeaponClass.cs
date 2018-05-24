using System;

namespace DoorKickersWeaponStat
{
    [Flags]
    public enum WeaponClass
    {
        None = 0,
        Pointman = 1 << 0,
        Assaulter = 1 << 1,
        Breacher = 1 << 2,
        Stealth = 1 << 3,
        Shield = 1 << 4,
        PointmanEnemy = 1 << 5,
        AssaultEnemy = 1 << 6,
    }
}

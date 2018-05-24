using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorKickersWeaponStat
{
    public class Firearm
    {
        public Firearm(
                string name,
                WeaponCategory weaponCategory,
                WeaponClass weaponClass,
                InventorySlot inventorySlot,
                int unlockCost,
                int roundsPerMagazine,
                decimal roundsPerSecond,
                int damagePerBullet,
                int armorPiercingLevel,
                decimal muzzleDropDistanceMeters,
                int moveSpeedLocalModifierPercent,
                int turnSpeedLocalModifierPercent,
                int numPellets,
                decimal spreadAt10Meters,
                bool silenced,
                decimal shotSoundMeters,
                bool closedBolt,
                bool cyclicReload,
                int reloadTime,
                int reloadEmptyTime,
                int changeInTime,
                int changeOutTime,
                int readyTime,
                int guardTime
            )
        {
            Name = name;
            WeaponCategory = weaponCategory;
            WeaponClass = weaponClass;
            InventorySlot = inventorySlot;
            UnlockCost = unlockCost;
            RoundsPerMagazine = roundsPerMagazine;
            RoundsPerSecond = roundsPerSecond;
            DamagePerBullet = damagePerBullet;
            ArmorPiercingLevel = armorPiercingLevel;
            MuzzleDropDistanceMeters = muzzleDropDistanceMeters;
            MoveSpeedLocalModifierPercent = moveSpeedLocalModifierPercent;
            TurnSpeedLocalModifierPercent = turnSpeedLocalModifierPercent;
            NumPellets = numPellets;
            SpreadAt10Meters = spreadAt10Meters;
            Silenced = silenced;
            ShotSoundMeters = shotSoundMeters;
            ClosedBolt = closedBolt;
            CyclicReload = cyclicReload;
            ReloadTime = reloadTime;
            ReloadEmptyTime = reloadEmptyTime;
            ChangeInTime = changeInTime;
            ChangeOutTime = changeOutTime;
            ReadyTime = readyTime;
            GuardTime = guardTime;
        }

        public string Name { get; }
        public WeaponCategory WeaponCategory { get; }
        public WeaponClass WeaponClass { get; }
        public InventorySlot InventorySlot { get; }
        public int UnlockCost { get; }
        public int RoundsPerMagazine { get; }
        public decimal RoundsPerSecond { get; }
        public int DamagePerBullet { get; }
        public int ArmorPiercingLevel { get; }
        public decimal MuzzleDropDistanceMeters { get; }
        public int MoveSpeedLocalModifierPercent { get; }
        public int TurnSpeedLocalModifierPercent { get; }
        public int NumPellets { get; }
        public decimal SpreadAt10Meters { get; }
        public bool Silenced { get; }
        public decimal ShotSoundMeters { get; }
        public bool ClosedBolt { get; }
        public bool CyclicReload { get; }
        public int ReloadTime { get; }
        public int ReloadEmptyTime { get; }
        public int ChangeInTime { get; }
        public int ChangeOutTime { get; }
        public int ReadyTime { get; }
        public int GuardTime { get; }

        public override string ToString()
        {
            return $"[{WeaponCategory}] {Name}";
        }
    }
}

using SprintZero1.Entities.BombEntityFolder;
using SprintZero1.Entities.BoomerangEntity;
using SprintZero1.Entities.BowAndMagicFireEntity;
using SprintZero1.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Entities.LootableItemEntity
{
    /// <summary>
    /// This class is meant to handle the creation of the weapon entity after the user picks it up. It's not meant for drawing any of these entities specifically. 
    /// @author Aaron Heishman
    /// </summary>
    internal static class WeaponEntityBuilder
    {
        private static readonly Dictionary<EquipmentItem, Func<string, IWeaponEntity>> equipmentWithoutPlayerMap = new Dictionary<EquipmentItem, Func<string, IWeaponEntity>>() {
            { EquipmentItem.Bow, (equip) => new RegularBowEntity(equip) },
            { EquipmentItem.BetterBow, (equip) => new BetterBowEntity(equip) },
            { EquipmentItem.Bomb, (equip) => new BombEntity(equip)},
            { EquipmentItem.MagicalRod, (equip) => new MagicFireEntity(equip)},
        };

        private static readonly Dictionary<EquipmentItem, Func<string, IMovableEntity, IWeaponEntity>> equipmentWithPlayerMap = new Dictionary<EquipmentItem, Func<string, IMovableEntity, IWeaponEntity>>()
        {
            { EquipmentItem.Boomerang, (equipment, player) => new RegularBoomerangEntity(equipment, player) },
            { EquipmentItem.BetterBoomerang, (equipment, player) => new BetterBoomerangEntity(equipment, player) },
        };

        /// <summary>
        /// Creates The desired weapon entity that does not require a player as a parameter
        /// </summary>
        /// <param name="equipment">The equipment that is being created</param>
        /// <returns>A new instance of the weapon entity</returns>
        public static IWeaponEntity CreateWeaponEntity(EquipmentItem equipment)
        {
            Debug.Assert(equipmentWithoutPlayerMap.ContainsKey(equipment), $"Dictionary does not contain {equipment} as a key");
            return equipmentWithoutPlayerMap[equipment].Invoke($"{equipment}");
        }

        /// <summary>
        /// Creates the desired WeaponEntity that requires the player as a parameter
        /// </summary>
        /// <param name="equipment">The equipment that is being created</param>
        /// <param name="player">The player that uses the weapon</param>
        /// <returns>a new instance of the desired weapon entity</returns>
        public static IWeaponEntity CreateWeaponEntity(EquipmentItem equipment, IMovableEntity player)
        {
            Debug.Assert(equipmentWithoutPlayerMap.ContainsKey(equipment), $"Dictionary does not contain {equipment} as a key");
            return equipmentWithPlayerMap[equipment].Invoke($"{equipment}", player);
        }
    }
}

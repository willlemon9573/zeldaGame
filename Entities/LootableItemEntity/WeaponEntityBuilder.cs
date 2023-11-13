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
    /// Handles creating the equipment item objects for the player after the items are picked up
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
        /// Creates The desires Weapon Entity
        /// </summary>
        /// <param name="equipment">The equipment that is being created</param>
        /// <returns>A new instance of the weapon entity</returns>
        public static IWeaponEntity CreateWeaponEntity(EquipmentItem equipment)
        {
            Debug.Assert(equipmentWithoutPlayerMap.ContainsKey(equipment), $"Dictionary does not contain {equipment} as a key");
            return equipmentWithoutPlayerMap[equipment].Invoke($"{equipment}");
        }

        public static IWeaponEntity CreateWeaponEntity(EquipmentItem equipment, IMovableEntity player)
        {
            Debug.Assert(equipmentWithoutPlayerMap.ContainsKey(equipment), $"Dictionary does not contain {equipment} as a key");
            return equipmentWithPlayerMap[equipment].Invoke($"{equipment}", player);
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.InventoryFiles;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Managers
{
    internal static class PlayerInventoryManager
    {
        private static Dictionary<ICombatEntity, PlayerInventory> _playerInventoryMap = new Dictionary<ICombatEntity, PlayerInventory>();

        public static void AddPlayerInventory(ICombatEntity player, PlayerInventory playerInventory)
        {
            Debug.Assert(!_playerInventoryMap.ContainsKey(player), $"{player}'s inventory already exists");
            _playerInventoryMap.Add(player, playerInventory);
        }

        /// <summary>
        /// Change the player's usable weapon in item selection screen
        /// </summary>
        /// <param name="player">The player who's changing weapons</param>
        /// <param name="weapon">The weapon to change to</param>
        public static void ChangeUsableWeapon(ICombatEntity player, IWeaponEntity weapon)
        {
            _playerInventoryMap[player].ChangeUsableItem(weapon);
        }

        /// <summary>
        /// Updates the current stackable item stock of the user
        /// </summary>
        /// <param name="player">the player who looted the item</param>
        /// <param name="item">the item that was looted</param>
        /// <param name="amount">the total amount of the item</param>
        public static void PlayerLootedStackableItem(ICombatEntity player, IEntity item, int amount)
        {
            _playerInventoryMap[player].PickedUpStackableItem(item, amount);
        }

        /// <summary>
        /// Add the utility items like maps, compass, etc
        /// </summary>
        /// <param name="item">The item to be added to the inventory</param>
        public static void AddUtilityItem(IEntity item)
        {
            //TODO - Create IUtilityItem interface
            //TODO - Create utility item class
            // etc
        }

        public static void AddEquipmentItemToInventory(ICombatEntity player, IPlayerItem equipment)
        {

        }
        /// <summary>
        /// Upgrade the equipment item with the new item
        /// </summary>
        /// <param name="player">the player who will receive the new item</param>
        /// <param name="oldEquipmentEntity">The old equipment item that's being upgraded</param>
        /// <param name="upgradedEquipment">The new item that replaces the old item</param>
        public static void UpgradeEquipment(ICombatEntity player, IEntity oldEquipmentEntity, IPlayerItem upgradedEquipment)
        {
            _playerInventoryMap[player].ReplaceEquipmentWithUpgrade(oldEquipmentEntity, upgradedEquipment);
        }
    }
}

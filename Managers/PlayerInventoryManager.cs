using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.InventoryFiles;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Managers
{
    /// <summary>
    /// A manager to handle all the players inventory management needs.
    /// </summary>
    internal static class PlayerInventoryManager
    {
        /* NOTE: Asserts added for debugging purposes in case we run into an error */
        private static readonly Dictionary<IEntity, PlayerInventory> _playerInventoryMap = new Dictionary<IEntity, PlayerInventory>();
        /// <summary>
        /// Adds a new player and their respective inventory to the inventory manager
        /// </summary>
        /// <param name="player">The player to be added to the manager</param>
        /// <param name="playerInventory">the inventory of the player to be added to the manager</param>
        public static void AddPlayerInventory(IEntity player, PlayerInventory playerInventory)
        {
            Debug.Assert(!_playerInventoryMap.ContainsKey(player), $"{player}'s inventory already exists");
            Debug.Assert(player != null && playerInventory != null, "Error: player or player inventory is null.");
            _playerInventoryMap.Add(player, playerInventory);
        }

        /// <summary>
        /// Change the player's usable weapon in item selection screen
        /// </summary>
        /// <param name="player">The player who's changing weapons</param>
        /// <param name="weapon">The weapon to change to</param>
        public static void ChangeEquipmentItem(IEntity player, EquipmentItem newEquipment)
        {
            Debug.Assert(player != null, "Error: Player is null.");
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"Inventory manager could not find {player}");
            _playerInventoryMap[player].ChangeEquipmentItem(newEquipment);
        }

        /// <summary>
        /// Updates the current stackable item stock of the user
        /// </summary>
        /// <param name="player">the player who looted the item</param>
        /// <param name="item">the item that was looted</param>
        /// <param name="amount">the total amount of the item</param>
        public static void OnStackableItemPickup(IEntity player, Items item, int amount)
        {
            Debug.Assert(player != null, "Error: Player is null.");
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

        public static void AddEquipmentItemToInventory(IEntity player, IPlayerItem equipment)
        {
            Debug.Assert(player != null, "Error: Player is null.");
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"Inventory manager could not find {player}");
        }

        /// <summary>
        /// Upgrades one of the player's equipment items 
        /// Example: boomerang to better boomerang
        /// </summary>
        /// <param name="player">The player receiving the upgrade</param>
        /// <param name="oldEquipment">The old equipment to be removed (used as key)</param>
        /// <param name="upgradedEquipment">The new item being added (used as key)</param>
        /// <param name="upgradedEquipmentEntity">The entity object of the item being added</param>
        public static void UpgradeEquipment(IEntity player, EquipmentItem oldEquipment, EquipmentItem upgradedEquipment, IWeaponEntity upgradedEquipmentEntity)
        {
            Debug.Assert(player != null || upgradedEquipmentEntity != null, "Error: Player or upgradedEquipmentEntity is null.");
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"Inventory manager could not find {player}");
            _playerInventoryMap[player].UpgradePlayerEquipment(oldEquipment, upgradedEquipment, upgradedEquipmentEntity);
        }
    }
}

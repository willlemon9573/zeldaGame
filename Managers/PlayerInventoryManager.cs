using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.InventoryFiles;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Managers
{
    internal delegate void StackableItemHandler(IEntity player, StackableItems item, int amount);

    internal delegate void EquipmentItemHandler(IEntity player, EquipmentItem equipment, IWeaponEntity newEquipment);

    internal delegate void UtilityItemHandler(IEntity player, DungeonItems item);
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
        /// Use the item in the player inventory
        /// </summary>
        /// <param name="player">The player that's requesting the item to use</param>
        /// <param name="item">the item being used</param>
        /// <returns>true if the user has the item in stock, false otherwise</returns>
        public static void UseStackableItem(IEntity player, StackableItems item, int amount)
        {
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"{player} does not have an inventory.");
            Debug.Assert(amount >= 0, $"{amount} must be a positive value");
            _playerInventoryMap[player].UsedItem(item, amount);
            HUDManager.UpdateStackableItemCount(item, _playerInventoryMap[player].GetStackableItemCount(item));
        }

        /// <summary>
        /// Change the player's usable weapon in item selection screen
        /// </summary>
        /// <param name="player">The player who's changing weapons</param>
        /// <param name="newEquipment">the new equipment the player is changing to</param>
        public static void ChangeEquipment(IEntity player, EquipmentItem newEquipment)
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
        public static void AddStackableItemToInventory(IEntity player, StackableItems item, int amount)
        {
            Debug.Assert(player != null, "Error: Player is null.");
            _playerInventoryMap[player].AddItem(item, amount);
            HUDManager.UpdateStackableItemCount(item, _playerInventoryMap[player].GetStackableItemCount(item));
        }

        /// <summary>
        /// Add the utility items like maps, compass, etc
        /// </summary>
        /// <param name="item">The item to be added to the inventory</param>
        public static void AddUtilityItemToInventory(IEntity player, DungeonItems item)
        {
            Debug.Assert(player != null, "Error: Player is null.");
            Debug.WriteLine(_playerInventoryMap[player].IsInInventory(item));
            _playerInventoryMap[player].AddDungeonUtilityItem(item);
        }

        /// <summary>
        /// Check if a player contains a specific item in their inventory already
        /// </summary>
        /// <param name="player">The player to be checked</param>
        /// <param name="equipment">The equipment item to look for</param>
        /// <returns>True if the player contains the item, false otherwise</returns>
        public static bool PlayerContainsEquipment(IEntity player, EquipmentItem equipment)
        {
            return _playerInventoryMap[player].IsInInventory(equipment);
        }

        /// <summary>
        /// Add an equipment item to the palyer's inventory
        /// </summary>
        /// <param name="player">the player receiving the equipment</param>
        /// <param name="equipment">the enum to be used as the key to access the equipment</param>
        /// <param name="newEquipment">the enetity object for the player to access</param>
        public static void AddEquipmentItemToInventory(IEntity player, EquipmentItem equipment, IWeaponEntity newEquipment)
        {
            Debug.Assert(player != null, "Error: Player is null.");
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"Inventory manager could not find {player}");
            Debug.Assert(!_playerInventoryMap[player].IsInInventory(equipment), $"Error adding to innventory, {player} already contains {equipment}");
            _playerInventoryMap[player].AddNewEquipment(equipment, newEquipment);

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
            Debug.Assert(player != null || upgradedEquipmentEntity != null, "Error: Player or upgradedEquipmentEntity cannot be null.");
            Debug.Assert(_playerInventoryMap.ContainsKey(player), $"Error Upgrading equipment. {player} not found in inventory manager.");
            Debug.Assert(_playerInventoryMap[player].IsInInventory(oldEquipment), $"Error upgrading equipment. {player} does not contain {oldEquipment} in their inventory.");
            Debug.Assert(!_playerInventoryMap[player].IsInInventory(upgradedEquipment), $"Error adding to inventory, {player} already contains {upgradedEquipment}");
            _playerInventoryMap[player].UpgradeEquipment(oldEquipment, upgradedEquipment, upgradedEquipmentEntity);
        }

        /// <summary>
        /// Gets the current amount an item in the player's inventory
        /// </summary>
        /// <param name="player">The player who's accessing their inventory</param>
        /// <param name="item">The item that the player will use</param>
        /// <returns>The total count of the item specified from the player inventory</returns>
        public static int GetStackableItemCount(IEntity player, StackableItems item)
        {
            return _playerInventoryMap[player].GetStackableItemCount(item);
        }

        /// <summary>
        /// Get a list of the equipment the player currently has
        /// </summary>
        /// <returns>A list of enums related to the items in the player equipment inventory</returns>
        public static List<EquipmentItem> GetPlayerEquipmentList(IEntity player)
        {
            return _playerInventoryMap[player].GetEquipmentList();
        }

        /// <summary>
        /// Get the list of dungeon items owned by the player
        /// </summary>
        /// <returns>A list of the dungeon utility items the player has</returns>
        public static List<DungeonItems> GetPlayerDungeonItems(IEntity player)
        {
            return _playerInventoryMap[player].GetDungeonItems();
        }

        /// <summary>
        /// Resets all player inventories
        /// </summary>
        public static void Reset()
        {
            _playerInventoryMap.Clear();
        }
    }
}

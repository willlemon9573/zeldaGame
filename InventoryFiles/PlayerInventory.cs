using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.InventoryFiles
{
    internal class PlayerInventory
    {
        const string INVENTORY_DOCUMENT_PATH = @"XMLFiles\PlayerXMLFiles\StartingInventory.xml";
        const string DOCUMENT_ROOT = "startinginventory";
        const string STARTING_WEAPON_ELEMENT = "startingweapon";
        const string STACKABLE_ITEMS_ELEMENT = "stackableitems";
        /* ---------------------------------------- FIelds and properties ---------------------------------------- */
        private const int MAX_EQUIPMENT_SLOTS = 8;
        private const int MAX_UTILITY_SLOTS = 2; // set to two just because we only have 1 map and 1 compass to get
        private readonly PlayerEntity _inventoryOwner; // need the player as base state to not have to add a whole new interface to access weapon slots
        // slots for ruppees, bombs, etc
        private Dictionary<StackableItems, IStackableItems> _StackableItemSlots;
        // slots for the secondary items
        private readonly Dictionary<EquipmentItem, IWeaponEntity> _equipmentSlots = new Dictionary<EquipmentItem, IWeaponEntity>();
        // slots for the compass and map
        private readonly List<DungeonItems> _DungeonUtilityItemSlots = new List<DungeonItems>();

        /* ---------------------------------------- Private functions ---------------------------------------- */

        /// <summary>
        /// Used to create the player inventory 
        /// </summary>
        private void BuildPlayerInventory()
        {
            XDocument inventoryDocument = XDocument.Load(INVENTORY_DOCUMENT_PATH);
            InventoryXMLParser parser = new InventoryXMLParser(inventoryDocument, DOCUMENT_ROOT);
            _inventoryOwner.SwordSlot = parser.ParsePlayerWeapon(STARTING_WEAPON_ELEMENT);
            _StackableItemSlots = parser.ParseInitialStartingItems(STACKABLE_ITEMS_ELEMENT);
        }

        /* ---------------------------------------- Public Methods ---------------------------------------- */
        /// <summary>
        /// Create the inventory for 
        /// </summary>
        /// <param name="player">The player who will own the inventory</param>
        /// <param name="playerSwordRef">the reference to the player's weapon slot</param>
        /// <param name="playerEquipmentRef">the reference to the player equipment slot</param>
        public PlayerInventory(PlayerEntity player)
        {
            _inventoryOwner = player;
            BuildPlayerInventory();
        }

        /// <summary>
        /// Player picks up an item. Increment the item in the list
        /// </summary>
        /// <param name="item"The item being picked up</param>
        /// <param name="amount">The amount of the item used</param>
        public void AddItem(StackableItems item, int amount)
        {
            Debug.Assert(_StackableItemSlots.ContainsKey(item), $"Error: {item} is not a stackable item");
            _StackableItemSlots[item].PickedUpItem(amount);
        }

        /// <summary>
        /// Adds a dungeon item like map or compass to the player inventory
        /// </summary>
        /// <param name="dungeonItem">The item to add to the player inventory</param>
        public void AddDungeonUtilityItem(DungeonItems dungeonItem)
        {
            Debug.Assert(!_DungeonUtilityItemSlots.Contains(dungeonItem), $"Player already contains {dungeonItem} in their inventory.");
            Debug.Assert(_DungeonUtilityItemSlots.Count < MAX_UTILITY_SLOTS, "Player Utility Item Slots are full.");

            //checking if compass or map to add to screen
            String name = dungeonItem.ToString();
            if (name.Contains("Map"))
            {
                HUDManager.addMap();
            }
            else if (name.Contains("Compass")) {

                HUDManager.addTriforceMarker(); 
            }
            


            _DungeonUtilityItemSlots.Add(dungeonItem);
        }

        /// <summary>
        /// Add an equipment item to the list. 
        /// </summary>
        /// <param name="equipmentItem">The equipment item to add to the palyer inventory</param>
        public void AddNewEquipment(EquipmentItem equipmentItem, IWeaponEntity equipmentEntity)
        {
            Debug.Assert(_equipmentSlots.Count < MAX_EQUIPMENT_SLOTS, "Error. Player equipment slots are filled.");
            _equipmentSlots.Add(equipmentItem, equipmentEntity);
        }

        /// <summary>
        /// Access an item that is used by the player
        /// </summary>
        /// <param name="item">the item being requested to use</param>
        /// <param name="amount">the amount of the item being used</param>
        /// <returns>true if the player can use the item, false otherwise</returns>
        public bool UsedItem(StackableItems item, int amount)
        {
            Debug.Assert(_StackableItemSlots.ContainsKey(item), $"${item} is not a stackable use item.");
            int stock = _StackableItemSlots[item].CurrentStock;
            if (stock == 0 || stock - amount < 0) { return false; }
            _StackableItemSlots[item].UsedItem(amount);
            return true;

        }

        /* 
         * Note on upgrading. In the original game some images show the game tracks the old items, but they aren't in a usable state
         * As we are only doing a single level it's up to the team whether we want to do this or not.
         */
        /// <summary>
        /// Upgrade a player's equipment
        /// </summary>
        /// <param name="oldEquipmentItem">The equipment to be upgraded</param>
        /// <param name="newEquipmentItem">The new equipment item that will replace the old one</param>
        /// <exception cref="Exception">Throws exception if an error occurs while trying to replace equipment items</exception>
        public void UpgradeEquipment(EquipmentItem oldEquipmentItem, EquipmentItem newEquipmentItem, IWeaponEntity newWeaponEntity)
        {
            /* Update player equipment slot if current equipment is being upgraded */
            if (_inventoryOwner.EquipmentSlot == _equipmentSlots[oldEquipmentItem])
            {
                _inventoryOwner.EquipmentSlot = newWeaponEntity;
            }
            /* Remove and replace the kvp of the old equipment item with the new kvp of the new equipment item*/
            _equipmentSlots.Remove(oldEquipmentItem);
            _equipmentSlots.Add(newEquipmentItem, newWeaponEntity);
        }

        /// <summary>
        /// Upgrades the player sword to the new sword
        /// </summary>
        /// <param name="newSword"></param>
        public void UpgradePlayerSword(IWeaponEntity newSword)
        {
            Debug.Assert(newSword is SwordEntity, $"Erorr upgrading playersword. {newSword} is not a type of sword entity");
            _inventoryOwner.SwordSlot = newSword;
        }

        /// <summary>
        /// Change the player's current equipment item to a new equipment item
        /// </summary>
        /// <param name="newEquipment">The new item the player will use</param>
        public void ChangeEquipmentItem(EquipmentItem newEquipment)
        {
            Debug.Assert(_equipmentSlots.ContainsKey(newEquipment), $"The player does not contain {newEquipment} in their inventory.");
            _inventoryOwner.EquipmentSlot = _equipmentSlots[newEquipment];
        }

        /// <summary>
        /// Check if an item is in the player's inventory
        /// </summary>
        /// <param name="item">the item to be checked</param>
        /// <returns>True if the item is in the inventory, false otherwise</returns>
        public bool IsInInventory(EquipmentItem item)
        {
            return _equipmentSlots.ContainsKey(item);
        }

        /// <summary>
        /// Check if a dungeon utility item like the compass or map are in the player's inventory
        /// </summary>
        /// <param name="dungeonItems">The item to find</param>
        /// <returns>True if the item is in the player inventory, false otherwise</returns>
        public bool IsInInventory(DungeonItems dungeonItems)
        {
            return _DungeonUtilityItemSlots.Contains(dungeonItems);
        }

        /// <summary>
        /// Get the total count of an item in the player's inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetStackableItemCount(StackableItems item)
        {
            // No need for assert, player will be built to contain a slot for all stackable items
            return _StackableItemSlots[item].CurrentStock;
        }

        /// <summary>
        /// Get the list of Sprites and their related stocks
        /// </summary>
        /// <returns>A list that contains the Sprite information and current stock of the item in the player inventory</returns>
        public List<Tuple<ISprite, int>> GetStackableItemSpritesAndCount()
        {
            return _StackableItemSlots.Select(kvp => new Tuple<ISprite, int>(kvp.Value.ItemSprite, kvp.Value.CurrentStock)).ToList();
        }

        /// <summary>
        /// Get a list of the equipment the player currently has
        /// </summary>
        /// <returns>A list of enums related to the items in the player equipment inventory</returns>
        public List<EquipmentItem> GetEquipmentList()
        {
            return _equipmentSlots.Keys.ToList();
        }

        /// <summary>
        /// Get the list of dungeon items owned by the player
        /// </summary>
        /// <returns>A list of the dungeon utility items the player has</returns>
        public List<DungeonItems> GetDungeonItems()
        {
            return _DungeonUtilityItemSlots;
        }
    }
}

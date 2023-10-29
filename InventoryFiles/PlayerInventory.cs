using SprintZero1.Entities;
using SprintZero1.Enums;
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
        private const int MAX_UTILITY_SLOTS = 7;
        private readonly PlayerEntity _inventoryOwner; // need the player as base state to not have to add a whole new interface to access weapon slots
        private Dictionary<Items, IStackableItems> _playerStackableItemSlots;
        private readonly Dictionary<EquipmentItem, IWeaponEntity> _playerEquipmentSlots = new Dictionary<EquipmentItem, IWeaponEntity>();

        /* ---------------------------------------- Private functions ---------------------------------------- */

        /// <summary>
        /// Used to create the player inventory 
        /// </summary>
        private void BuildPlayerInventory()
        {
            XDocument inventoryDocument = XDocument.Load(INVENTORY_DOCUMENT_PATH);
            InventoryXMLParser parser = new InventoryXMLParser(inventoryDocument, DOCUMENT_ROOT);
            _inventoryOwner.SwordSlot = parser.ParsePlayerWeapon(STARTING_WEAPON_ELEMENT);
            _playerStackableItemSlots = parser.ParseInitialStartingItems(STACKABLE_ITEMS_ELEMENT);
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
        public void PickedUpStackableItem(Items item, int amount)
        {
            // No need for assert, player will be built to contain a slot for all stackable items
            _playerStackableItemSlots[item].PickedUpItem(amount);
        }

        /// <summary>
        /// Adds a dungeon item like map or compass to the player inventory
        /// </summary>
        /// <param name="dungeonItem">The item to add to the player inventory</param>
        public void AddDungeonItemToInventory(DungeonItems dungeonItem)
        {

        }

        /// <summary>
        /// Add an equipment item to the list. 
        /// </summary>
        /// <param name="equipmentItem">The equipment item to add to the palyer inventory</param>
        public void AddEquipmentItem(EquipmentItem equipmentItem, IWeaponEntity equipmentEntity)
        {
            Debug.Assert(_playerEquipmentSlots.Count < MAX_EQUIPMENT_SLOTS, "Error. Player equipment slots are filled.");
            _playerEquipmentSlots[equipmentItem] = equipmentEntity;
        }

        /* 
         * Note on upgrading. In the original game some images show the game tracks the old items, but they are in a usable state
         * As we are only doing a single level it's up to the team whether we want to do this or not.
         */

        /// <summary>
        /// Upgrade a player's equipment
        /// </summary>
        /// <param name="oldEquipmentItem">The equipment to be upgraded</param>
        /// <param name="newEquipmentItem">The new equipment item that will replace the old one</param>
        /// <exception cref="Exception">Throws exception if an error occurs while trying to replace equipment items</exception>
        public void UpgradePlayerEquipment(EquipmentItem oldEquipmentItem, EquipmentItem newEquipmentItem, IWeaponEntity newWeaponEntity)
        {
            /* Update player equipment slot if current equipment is being upgraded */
            if (_inventoryOwner.EquipmentSlot == _playerEquipmentSlots[oldEquipmentItem])
            {
                _inventoryOwner.EquipmentSlot = newWeaponEntity;
            }
            /* Remove and replace the kvp of the old equipment item with the new kvp of the new equipment item*/
            _playerEquipmentSlots.Remove(oldEquipmentItem);
            _playerEquipmentSlots.Add(newEquipmentItem, newWeaponEntity);

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
            _inventoryOwner.EquipmentSlot = _playerEquipmentSlots[newEquipment];
        }

        /// <summary>
        /// Check if an item is in the player's inventory
        /// </summary>
        /// <param name="item">the item to be checked</param>
        /// <returns>True if the item is in the inventory, false otherwise</returns>
        public bool IsInInventory(EquipmentItem item)
        {
            return _playerEquipmentSlots.ContainsKey(item);
        }

        public int GetStackableItemCount(Items item)
        {
            // No need for assert, player will be built to contain a slot for all stackable items
            return _playerStackableItemSlots[item].CurrentStock;
        }

        /// <summary>
        /// Get the list of Sprites and their related stocks
        /// </summary>
        /// <returns>A list that contains the Sprite information and current stock of the item in the player inventory</returns>
        public List<Tuple<ISprite, int>> GetStackableItemSpritesAndCount()
        {
            return _playerStackableItemSlots.Select(kvp => new Tuple<ISprite, int>(kvp.Value.ItemSprite, kvp.Value.CurrentStock)).ToList();
        }
    }
}

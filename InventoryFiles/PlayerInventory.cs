using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.InventoryFiles
{
    internal class PlayerInventory
    {
        /* ---------------------------------------- FIelds and properties ---------------------------------------- */
        private const int MAX_EQUIPMENT_SLOTS = 8;
        private const int MAX_UTILITY_SLOTS = 7;
        private readonly IEntity _inventoryOwner; // player may not be used, will remove if we don't find a use. 
        private readonly Dictionary<Items, IStackableItems> _playerStackableItemSlots = new Dictionary<Items, IStackableItems>();
        private readonly Dictionary<EquipmentItem, IWeaponEntity> _playerEquipmentSlots = new Dictionary<EquipmentItem, IWeaponEntity>();
        /* Player already contains a large amount of properties and fields as is.
         * So these will hold a reference to the player's current sword slot and equipment slot
         * For easily changing the players items around
         */
        private IWeaponEntity _playerSwordSlotReference;
        private IWeaponEntity _playerEquipmentSlotReference;
        /* ---------------------------------------- Private functions ---------------------------------------- */

        /// <summary>
        /// Used to create the player inventory 
        /// </summary>
        private void BuildPlayerInventory()
        {
            // TODO - Parse xml file with player's inventory 
        }

        /* ---------------------------------------- Public Methods ---------------------------------------- */
        /// <summary>
        /// Create the inventory for 
        /// </summary>
        /// <param name="player">The player who will own the inventory</param>
        /// <param name="playerSwordRef">the reference to the player's weapon slot</param>
        /// <param name="playerEquipmentRef">the reference to the player equipment slot</param>
        public PlayerInventory(ICombatEntity player, ref IWeaponEntity playerSwordRef, ref IWeaponEntity playerEquipmentRef)
        {
            _inventoryOwner = player;
            _playerSwordSlotReference = playerSwordRef;
            _playerEquipmentSlotReference = playerEquipmentRef;
        }

        /// <summary>
        /// Player picks up an item. Increment the item in the list
        /// </summary>
        /// <param name="item"The item being picked up</param>
        /// <param name="amount">The amount of the item used</param>
        public void PickedUpStackableItem(Items item, int amount)
        {
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
        /// Add an equipment item to the list
        /// </summary>
        /// <param name="equipmentItem"></param>
        public void AddEquipmentItem(EquipmentItem equipmentItem, IWeaponEntity equipmentEntity)
        {
            Debug.Assert(!_playerEquipmentSlots.ContainsKey(equipmentItem), $"Error adding {equipmentItem} to inventory. Item already exists in the inventory");
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
            Debug.Assert(_playerEquipmentSlots.ContainsKey(oldEquipmentItem), $"Error replacing equipment: {oldEquipmentItem} not located in player inventory");
            Debug.Assert(!_playerEquipmentSlots.ContainsKey(newEquipmentItem), $"Erorr replacing equipment: {newEquipmentItem} already exists in player inventory.");
            /* Check if the equipment being replaced is the same one the user is currently used */
            if (_playerEquipmentSlotReference == _playerEquipmentSlots[oldEquipmentItem])
            {
                _playerEquipmentSlotReference = newWeaponEntity;
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
            _playerSwordSlotReference = newSword;
        }

        /// <summary>
        /// Change the player's current equipment item to a new equipment item
        /// </summary>
        /// <param name="newEquipment">The new item the player will use</param>
        public void ChangeEquipmentItem(EquipmentItem newEquipment)
        {
            Debug.Assert(_playerEquipmentSlots.ContainsKey(newEquipment));
            _playerEquipmentSlotReference = _playerEquipmentSlots[newEquipment];
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

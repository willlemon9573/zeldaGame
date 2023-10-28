using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.InventoryFiles
{
    internal class PlayerInventory
    {
        /* ---------------------------------------- FIelds and properties ---------------------------------------- */
        private const int MAX_EQUIPMENT_SLOTS = 8;
        private const int MAX_UTILITY_SLOTS = 7;
        private IEntity _inventoryOwner;
        private Dictionary<Items, IStackableItems> _playerStackableItemSlots = new Dictionary<Items, IStackableItems>();
        private Dictionary<EquipmentItem, IWeaponEntity> _playerEquipmentSlots = new Dictionary<EquipmentItem, IWeaponEntity>();
        private IWeaponEntity _playerSwordSlotReference;
        private IWeaponEntity _playerEquipmentSlotReference;
        /* ---------------------------------------- Private functions ---------------------------------------- */

        /// <summary>
        /// Used to create the player inventory 
        /// </summary>
        private void BuildPlayerInventory()
        {

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


        /// <summary>
        /// Replace an equipment item with its upgraded item version
        /// </summary>
        /// <param name="oldEquipmentItem">The equipment to be replaced</param>
        /// <param name="newEquipmentItem">The new equipment item to be added to inventory</param>
        /// <exception cref="System.Exception">Throws exception if an error occurs while trying to replace equipment items</exception>
        public void ReplaceEquipmentWithUpgrade(EquipmentItem oldEquipmentItem, EquipmentItem newEquipmentItem, IWeaponEntity newWeaponEntity)
        {
            Debug.Assert(_playerEquipmentSlots.ContainsKey(oldEquipmentItem), $"Error replacing equipment: {oldEquipmentItem} not located in player inventory");
            Debug.Assert(!_playerEquipmentSlots.ContainsKey(newEquipmentItem), $"Erorr replacing equipment: {newEquipmentItem} already exists in player inventory.");
            if (_playerEquipmentSlots.Remove(oldEquipmentItem))
            {
                _playerEquipmentSlots.Add(newEquipmentItem, newWeaponEntity);
            }
            else
            {
                throw new System.Exception($"Error removing {oldEquipmentItem} and replacing with {newEquipmentItem}");
            }
        }

        public void ChangeEquipmentItem(EquipmentItem newEquipment)
        {
            Debug.Assert(_playerEquipmentSlots.ContainsKey(newEquipment));
            _playerEquipmentSlotReference = _playerEquipmentSlots[newEquipment];
        }

        /// <summary>
        /// Retrieve the sprites for all stackable items
        /// </summary>
        /// <returns>An IEnumeral of sprite objects that represent the sprites of each stackable item in the player inventory</returns>
        //public List<Tuple<ISprite, int>> GetStackableItemSpritesAndCount()
        //{
        //return StackableItem => _playerStackableItemSlots.Select(kvp => new Tuple<ISprite, int>(kvp.Value.ItemSprite, kvp.Value.CurrentStock));
        //}
    }
}

using SprintZero1.Entities;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.InventoryFiles
{
    internal class PlayerInventory
    {
        /* ---------------------------------------- FIelds and properties ---------------------------------------- */
        private const int MAX_EQUIPMENT_SLOTS = 8;
        private const int MAX_UTILITY_SLOTS = 7;
        /// <summary>
        /// Holds all the items that are stackable like projectile ammo, ruppees, etc
        /// </summary>
        private List<IStackableItems> _stackableItemSlots;
        /// <summary>
        /// Holds the utility items like the maps, compass, triforce, etc
        /// </summary>
        private List<IPlayerItem> _UtilityItemSlots;
        /// <summary>
        /// Holds the player's equipment
        /// </summary>
        private List<IPlayerItem> _EquipmentSlots;
        /// <summary>
        /// The player that owns the inventory
        /// </summary>
        private readonly ICombatEntity _inventoryOwner;
        private IWeaponEntity[] _playerWeaponSlots;

        /* ---------------------------------------- Private functions ---------------------------------------- */

        /// <summary>
        /// returns the index of the item
        /// </summary>
        /// <param name="list">The list to iterate over</param>
        /// <param name="itemEntity">The item being looked for</param>
        /// <returns>The item index if it's in the list, else returns -1</returns>
        private int IndexOfPlayerItem(List<IPlayerItem> list, IEntity itemEntity)
        {

            foreach (IPlayerItem item in list)
            {
                if (item.ItemEntity == itemEntity)
                {
                    return list.IndexOf(item);
                }
            }
            return -1;

        }

        /// <summary>
        /// Returns the index of the stackable item
        /// </summary>
        /// <param name="stackableItem">The item to look for</param>
        /// <returns>The index of the stackable item if it exists else -1</returns>
        private int IndexOfStackableItem(IEntity stackableItem)
        {
            foreach (StackableItem item in _stackableItemSlots)
            {
                if (item.ItemEntity == stackableItem)
                {
                    return _stackableItemSlots.IndexOf(item);
                }
            }
            return -1;
        }
        /* ---------------------------------------- Public Methods ---------------------------------------- */
        public PlayerInventory(ICombatEntity player, ref IWeaponEntity playerWeaponRef, ref IWeaponEntity secondaryWeaponRef)
        {
            _inventoryOwner = player;
            _playerWeaponSlots = new IWeaponEntity[] { playerWeaponRef, secondaryWeaponRef };
        }

        /// <summary>
        /// builds the inventories that will hold the items
        /// </summary>
        public void BuildPlayerInventory()
        {
            // TODO - Build each inventory slot
            // TODO - Add the default starting weapon
            // TODO - Add the default use Item weapon?
        }

        /// <summary>
        /// Player picks up an item. Increment the item in the list
        /// </summary>
        /// <param name="itemEntity">The item that was used</param>
        /// <param name="amount">The amount of the item used</param>
        public void PickedUpStackableItem(IEntity stackableItem, int amount)
        {
            int i = IndexOfStackableItem(stackableItem);
            _stackableItemSlots[i].PickedUpItem(amount);
        }

        public void AddUtilityItem(IUtilityItem utilityItem)
        {
            Debug.Assert(_UtilityItemSlots.Count <= MAX_UTILITY_SLOTS);

        }
        /// <summary>
        /// Add an equipment item to the list
        /// </summary>
        /// <param name="equipmentItem"></param>
        public void AddEquipmentItem(IPlayerItem equipmentItem)
        {
            Debug.Assert(equipmentItem != null, $"{nameof(equipmentItem)} cannot be null");
            Debug.Assert(!_EquipmentSlots.Contains(equipmentItem), $"PlayerInventory already contains {equipmentItem}");
            Debug.Assert(_EquipmentSlots.Count < MAX_EQUIPMENT_SLOTS, $"Player inventory cannot hold more than {MAX_EQUIPMENT_SLOTS} items");
            _EquipmentSlots.Add(equipmentItem);
        }

        /// <summary>
        /// Replace an equipment item with its upgraded item version
        /// </summary>
        /// <param name="oldEquipmentItem">The equipment to be replaced</param>
        /// <param name="newEquipmentItem">The new equipment item to be added to inventory</param>
        public void ReplaceEquipmentWithUpgrade(IEntity oldEquipmentItem, IPlayerItem newEquipmentItem)
        {
            int index = IndexOfPlayerItem(_EquipmentSlots, oldEquipmentItem);
            _EquipmentSlots[index] = newEquipmentItem;
            if (_playerWeaponSlots[1] == (IWeaponEntity)oldEquipmentItem)
            {
                _playerWeaponSlots[1] = (IWeaponEntity)newEquipmentItem.ItemEntity;
            }
        }

        public void ChangeUsableItem(IEntity equipmentItem)
        {
            Debug.Assert(equipmentItem != null, $"{nameof(equipmentItem)} cannot be null.");
            int index = IndexOfPlayerItem(_EquipmentSlots, equipmentItem);
            _playerWeaponSlots[1] = (IWeaponEntity)_EquipmentSlots[index];
        }

        /// <summary>
        /// Retrieve the sprites for all stackable items
        /// </summary>
        /// <returns>An IEnumeral of sprite objects that represent the sprites of each stackable item in the player inventory</returns>
        public IEnumerable<ISprite> GetStackableItemSprites()
        {
            foreach (var item in _stackableItemSlots)
            {
                yield return item.ItemSprite;
            }
        }
    }
}

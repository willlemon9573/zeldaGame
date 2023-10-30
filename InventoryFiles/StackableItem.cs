using SprintZero1.Entities;
using SprintZero1.Sprites;
using System.Diagnostics;

namespace SprintZero1.InventoryFiles
{
    internal class StackableItem : IStackableItems
    {
        // TODO - Need to remove certain things like the ItemEntity itself
        private int _currentStock;
        private readonly int _maxStock;
        private readonly IEntity _itemEntity;
        private readonly ISprite _itemsprite;

        public int CurrentStock { get { return _currentStock; } }

        public int MaxStock { get { return _maxStock; } }

        public IEntity ItemEntity { get { return _itemEntity; } }

        public ISprite ItemSprite { get { return _itemsprite; } }

        /* Note IENTITY may not even be needed */
        /// <summary>
        /// Creates a stackable item that can be placed in a player inventory
        /// </summary>
        /// <param name="startingStock">The starting amount the player will start with</param>
        /// <param name="maxStock">the max stock of the item</param>
        /// <param name="itemEntity">the entity object the item belongs to</param>
        /// <param name="itemsprite">The sprite of the item</param>
        public StackableItem(int startingStock, int maxStock, IEntity itemEntity, ISprite itemsprite)
        {
            _currentStock = startingStock;
            _maxStock = maxStock;
            _itemEntity = itemEntity;
            _itemsprite = itemsprite;
        }

        /// <summary>
        /// Add the specified amount to the stock
        /// </summary>
        /// <param name="amount">the amount to be added to the inventory</param>
        public void PickedUpItem(int amount)
        {
            if (_currentStock == MaxStock) { return; }
            _currentStock += amount;
            if (_currentStock > MaxStock) { _currentStock = MaxStock; }
        }

        /// <summary>
        /// Remove the specified amount from the current stock
        /// </summary>
        /// <param name="amount">the amount to remove</param>
        public void UsedItem(int amount)
        {
            Debug.Assert(_currentStock - amount >= 0, $"{CurrentStock} - {amount} < 0");
            _currentStock -= amount;
        }
    }
}

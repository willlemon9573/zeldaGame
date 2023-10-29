using SprintZero1.Entities;
using SprintZero1.Sprites;

namespace SprintZero1.InventoryFiles
{
    internal class StackableItem : IStackableItems
    {
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
            if (_currentStock == MaxStock)
            {
                return;
            }
            else if (_currentStock + amount > MaxStock)
            {
                _currentStock = MaxStock;
            }
            else
            {
                _currentStock += amount;
            }
        }
        /// <summary>
        /// Remove the specified amount from the stock
        /// </summary>
        /// <param name="amount">the amount to remove</param>
        public void UsedItem(int amount)
        {
            if (_currentStock == 0)
            {
                return;
            }
            else if (_currentStock - amount < 0)
            {
                _currentStock = 0;
            }
            else
            {
                _currentStock -= amount;
            }
        }
    }
}

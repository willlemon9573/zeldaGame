using SprintZero1.Entities;
using SprintZero1.Sprites;

namespace SprintZero1.InventoryFiles
{
    internal class StackableItem : IStackableItems
    {
        private int _currentStock;
        private int _maxStock;
        private IEntity _itemEntity;
        private ISprite _itemsprite;

        public int CurrentStock { get { return _currentStock; } }

        public int MaxStock { get { return _maxStock; } }

        public IEntity ItemEntity { get { return _itemEntity; } }

        public ISprite ItemSprite { get { return _itemsprite; } }

        public StackableItem(int currentStock, int maxStock, IEntity itemEntity, ISprite itemsprite)
        {
            _currentStock = currentStock;
            _maxStock = maxStock;
            _itemEntity = itemEntity;
            _itemsprite = itemsprite;
        }

        public void PickedUpItem(int amount)
        {
            _currentStock += amount;
        }

        public void UsedItem(int amount)
        {
            _currentStock -= amount;
        }
    }
}

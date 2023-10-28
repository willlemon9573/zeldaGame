﻿using SprintZero1.Entities;
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

using SprintZero1.Entities;
using SprintZero1.Sprites;

namespace SprintZero1.InventoryFiles
{
    internal class EquipmentItem : IPlayerItem
    {
        private IEntity _equipmentEntity;
        private ISprite _itemSprite;
        public IEntity ItemEntity { get { return _equipmentEntity; } }

        public ISprite ItemSprite { get { return _itemSprite; } }

        public EquipmentItem(IEntity equipmentEntity, ISprite itemSprite)
        {
            _equipmentEntity = equipmentEntity;
            _itemSprite = itemSprite;
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class EquipmentItemEntity : LootableItemBase
    {
        private readonly EquipmentItem _equipmentItem;
        private readonly RemoveDelegate _remove;
        private readonly EquipmentItemHandler _pickupHandler;

        public EquipmentItemEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate, EquipmentItemHandler pickupHandler, EquipmentItem itemType) : base(entitySprite, position, removeDelegate)
        {
            _equipmentItem = itemType;
            _remove = removeDelegate;
            _pickupHandler = pickupHandler;
        }

        public override void Pickup(IEntity player, int amt = 0)
        {

        }
    }
}

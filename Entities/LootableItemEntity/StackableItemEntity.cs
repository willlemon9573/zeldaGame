using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class StackableItemEntity : LootableItemBase
    {
        readonly StackableItemHandler _pickupHandler;
        readonly StackableItems _itemType;
        public StackableItemEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate, StackableItemHandler pickupHandler, StackableItems itemType) : base(entitySprite, position, removeDelegate)
        {
            _pickupHandler = pickupHandler;
            _itemType = itemType;
        }

        public override void Pickup(IEntity player, int amt = 0)
        {
            _pickupHandler(player, _itemType, amt);

        }
    }
}

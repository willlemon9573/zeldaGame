using Microsoft.Xna.Framework;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class RoomBeatKeyEvent : EnemyDefeatEventBase
    {
        Vector2 _dropPosition;
        public RoomBeatKeyEvent(DungeonRoom room, Vector2 dropPosition) : base(room)
        {
            this._roomWithEvent = room;
            this._dropPosition = dropPosition;
        }

        public ILootableEntity CreateKey()
        {
            string spriteName = "key";
            ISprite sprite = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(spriteName);
            RemoveDelegate remover = _roomWithEvent.RemoveAndSaveItem;
            StackableItemHandler itemHandler = PlayerInventoryManager.AddStackableItemToInventory;
            return new StackableItemEntity(sprite, _dropPosition, remover, itemHandler, StackableItems.DungeonKey);
        }

        public override void TriggerEvent()
        {
            if (this._roomWithEvent.LiveEnemyList.Count == 0 && this._canTriggerEvent)
            {
                _roomWithEvent.AddRoomItem(CreateKey());
                _canTriggerEvent = false;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class RoomBeatBoomerangEvent : EnemyDefeatEventBase
    {
        private readonly Vector2 _dropPosition;

        public RoomBeatBoomerangEvent(DungeonRoom room, Vector2 DropPosition) : base(room)
        {
            this._roomWithEvent = room;
            this._canTriggerEvent = true;
            this._dropPosition = DropPosition;
        }

        /// <summary>
        /// Creates the boomerang as a lootable entity
        /// </summary>
        /// <returns>The lootable entity version of the boomerang</returns>
        private ILootableEntity CreateBoomerang()
        {
            ISprite boomerangSprite = WeaponSpriteFactory.Instance.CreateBoomerangSprite("boomerang");
            RemoveDelegate itemRemover = this._roomWithEvent.RemoveAndSaveItem;
            EquipmentItemHandler itemHandler = PlayerInventoryManager.AddEquipmentItemToInventory;
            return new EquipmentItemWithPlayerEntity(boomerangSprite, _dropPosition, itemRemover, itemHandler, EquipmentItem.Boomerang);
        }

        public override void TriggerEvent()
        {
            int enemyCount = _roomWithEvent.LiveEnemyList.Count;
            if (enemyCount == 0 && this._canTriggerEvent)
            {
                _roomWithEvent.AddRoomItem(CreateBoomerang());
                _canTriggerEvent = false;
            }
        }
    }
}

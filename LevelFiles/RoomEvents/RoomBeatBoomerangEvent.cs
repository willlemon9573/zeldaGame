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
        private Vector2 _dropPosition;

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
        private ILootableEntity CreateBoomerang(int offset)
        {
            ISprite boomerangSprite = WeaponSpriteFactory.Instance.CreateBoomerangSprite("boomerang");
            RemoveDelegate itemRemover = this._roomWithEvent.RemoveAndSaveItem;
            EquipmentItemHandler itemHandler = PlayerInventoryManager.AddEquipmentItemToInventory;
            _dropPosition.X += offset;
            return new EquipmentItemWithPlayerEntity(boomerangSprite, _dropPosition, itemRemover, itemHandler, EquipmentItem.Boomerang);
        }

        public override void TriggerEvent()
        {
            int enemyCount = _roomWithEvent.LiveEnemyList.Count;
            if (enemyCount == 0 && this._canTriggerEvent)
            {
                _roomWithEvent.AddRoomItem(CreateBoomerang(offset: 0));
                _roomWithEvent.AddRoomItem(CreateBoomerang(offset: 50)); // place player2 boomerang by player1's boomerang
                _canTriggerEvent = false;
                SoundFactory.PlaySound(SoundFactory.GetSound("secret"));
            }
        }
    }
}

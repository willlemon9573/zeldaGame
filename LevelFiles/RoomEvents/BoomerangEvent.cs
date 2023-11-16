using Microsoft.Xna.Framework;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class DropBoomerangEvent : IRoomEvent
    {
        const int DropPositionX = 150;
        const int DropPositionY = 40;
        private bool _eventTriggered;
        private readonly DungeonRoom _dungeonRoom;

        /// <summary>
        /// Creates the boomerang as a lootable entity
        /// </summary>
        /// <returns>The lootable entity version of the boomerang</returns>
        private ILootableEntity CreateBoomerang()
        {
            ISprite boomerangSprite = WeaponSpriteFactory.Instance.CreateBoomerangSprite("boomerang");
            Vector2 mapPlacement = new Vector2(DropPositionX, DropPositionY);
            RemoveDelegate itemRemover = _dungeonRoom.RemoveAndSaveItem;
            EquipmentItemHandler itemHandler = PlayerInventoryManager.AddEquipmentItemToInventory;
            return new EquipmentItemWithPlayerEntity(boomerangSprite, mapPlacement, itemRemover, itemHandler, EquipmentItem.Boomerang);
        }

        /// <summary>
        /// Constructor for a room event to handle dropping a map in the given map
        /// </summary>i
        /// <param name="dungeonRoom">The room where the </param>
        public DropBoomerangEvent(DungeonRoom dungeonRoom)
        {
            _eventTriggered = false;
            _dungeonRoom = dungeonRoom;
        }

        public bool CanTriggerEvent()
        {
            return _eventTriggered;
        }

        public void TriggerEvent()
        {
            if (!CanTriggerEvent())
            {
                _dungeonRoom.AddRoomItem(CreateBoomerang());
                _eventTriggered = true;
            }
        }
    }
}

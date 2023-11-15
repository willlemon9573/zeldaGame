using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.LevelFiles.RoomEvents
{
    /// <summary>
    /// Handles the event for moving the block to unlock one of the rooms
    /// </summary>
    internal class MoveBlockEvent : IRoomEvent
    {
        private const int MaxDistance = 16;
        private readonly Vector2 _distance = new Vector2(0, 1);
        private readonly int MovingDistance;
        private readonly DungeonRoom _room;
        private readonly IMovableEntity _block;
        private bool _isEventTriggered;
        private readonly BlockedDoorEntity _doorToOpen;

        private void OpenDoors()
        {
            string doorType = $"open_{_doorToOpen.DoorDirection}";
            ISprite openDoorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(doorType.ToLower());
            BlockedDoorEntity newDoor = new BlockedDoorEntity(openDoorSprite, _doorToOpen.Position, _doorToOpen.DoorDestination, _doorToOpen.DoorDirection);
            _room.UpdateDoor(_doorToOpen, newDoor);
        }

        public MoveBlockEvent(DungeonRoom room, IMovableEntity block, BlockedDoorEntity door)
        {
            _room = room;
            _block = block;
            _isEventTriggered = false;
            _doorToOpen = door;
            MovingDistance = (int)block.Position.Y + MaxDistance;
        }

        public bool CanTriggerEvent()
        {
            return _isEventTriggered;
        }

        public void TriggerEvent()
        {
            if ((int)_block.Position.Y >= MovingDistance)
            {
                _block.Position -= _distance;
            }
            else
            {
                OpenDoors();
                _isEventTriggered = true;
            }
        }
    }
}

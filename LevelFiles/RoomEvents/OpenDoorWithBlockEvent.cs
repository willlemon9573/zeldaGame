using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;

namespace SprintZero1.LevelFiles.RoomEvents
{
    /// <summary>
    /// Handles the event for moving the block to unlock one of the rooms
    /// </summary>
    internal class OpenDoorWithBlockEvent : IRoomEvent
    {
        private readonly DungeonRoom _room;
        private readonly Direction _doorDirection;
        private bool _canTriggerEvent;
        private readonly IMovableEntity _movableBlock;
        private Vector2 _triggerPosition;

        /// <summary>
        /// Create a new instance of the open door with block event
        /// </summary>
        /// <param name="room">The room where the event will be triggered</param>
        /// <param name="movableBlock">the block used to trigger the event</param>
        /// <param name="triggerPosition">the position the block needs to be in to trigger the event</param>
        /// <param name="doorToOpenDirection">The direction of the door that will open</param>
        public OpenDoorWithBlockEvent(DungeonRoom room, IMovableEntity movableBlock, Vector2 triggerPosition, Direction doorToOpenDirection)
        {
            _room = room;
            _canTriggerEvent = true;
            _doorDirection = doorToOpenDirection;
            _movableBlock = movableBlock;
            _triggerPosition = triggerPosition;

        }

        public virtual bool CanTriggerEvent()
        {
            return _canTriggerEvent;
        }

        /// <summary>
        /// Triggers the room event that unlocks the door
        /// </summary>
        public virtual void TriggerEvent()
        {
            if (_movableBlock.Position == _triggerPosition)
            {
                _room.UnlockDoor(_doorDirection);
                _canTriggerEvent = false;
                SoundFactory.PlaySound(SoundFactory.GetSound("secret"));
            }
        }
    }
}

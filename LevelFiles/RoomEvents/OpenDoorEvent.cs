using SprintZero1.Enums;

namespace SprintZero1.LevelFiles.RoomEvents
{
    /// <summary>
    /// Handles the event for moving the block to unlock one of the rooms
    /// </summary>
    internal class OpenDoorEvent : IRoomEvent
    {
        private readonly DungeonRoom _room;
        private readonly Direction _doorDirection;
        private bool _isEventTriggered;

        /// <summary>
        /// Create a new instance of a MoveBlockEvent that opens a blocked door.
        /// </summary>
        /// <param name="room">The room that the event belongs to</param>
        /// <param name="doorToOpenDirection">The direction of the door to open in the room</param>
        public OpenDoorEvent(DungeonRoom room, Direction doorToOpenDirection)
        {
            _room = room;
            _isEventTriggered = false;
            _doorDirection = doorToOpenDirection;
        }

        public bool CanTriggerEvent()
        {
            return _isEventTriggered;
        }

        /// <summary>
        /// Triggers the room event that unlocks the door
        /// </summary>
        public void TriggerEvent()
        {
            _room.UnlockDoor(_doorDirection);
            _isEventTriggered = true;
        }
    }
}

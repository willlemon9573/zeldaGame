using SprintZero1.LevelFiles.RoomEvents;

namespace SprintZero1.LevelFiles
{
    internal class RoomWithEvent : DungeonRoom
    {
        private IRoomEvent _roomEvent;

        public void AddRoomEvent(IRoomEvent roomEvent)
        {
            _roomEvent = roomEvent;
        }

        public void TriggerEvent()
        {
            if (_roomEvent.CanTriggerEvent() == false) { return; }
            _roomEvent.TriggerEvent();
        }
    }
}

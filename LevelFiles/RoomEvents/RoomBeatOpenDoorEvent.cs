using SprintZero1.Enums;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class RoomBeatOpenDoorEvent : EnemyDefeatEventBase
    {
        private readonly Direction _doorToUnlockDirection;
        public RoomBeatOpenDoorEvent(DungeonRoom room, Direction doorToUnlockDirection) : base(room)
        {
            this._doorToUnlockDirection = doorToUnlockDirection;
            this._canTriggerEvent = true;
            this._roomWithEvent = room;
        }

        public override void TriggerEvent()
        {
            if (this._roomWithEvent.LiveEnemyList.Count == 0 && _canTriggerEvent)
            {
                this._roomWithEvent.UnlockDoor(_doorToUnlockDirection);
                this._canTriggerEvent = false;
            }
        }
    }
}

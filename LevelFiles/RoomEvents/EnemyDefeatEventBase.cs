using SprintZero1.Entities;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal abstract class EnemyDefeatEventBase : IRoomEvent
    {
        protected DungeonRoom _roomWithEvent;
        protected bool _canTriggerEvent;
        /// <summary>
        /// Inheritable function to for entities that are created through events
        /// so that they can be collided with for pickup
        /// </summary>
        /// <param name="entity">The entity to be added to the list</param>
        protected void AddEntityCollider(IEntity entity)
        {
            if (GameStatesManager.CurrentState is GamePlayingState playingState)
            {
                playingState.AddCollider(entity);
            }
        }
        protected EnemyDefeatEventBase(DungeonRoom room)
        {
            _roomWithEvent = room;
            _canTriggerEvent = true;
        }

        public bool CanTriggerEvent()
        {
            return _canTriggerEvent;
        }

        public abstract void TriggerEvent();
    }
}

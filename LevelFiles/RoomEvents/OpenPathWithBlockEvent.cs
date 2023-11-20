using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SprintZero1.Entities;
using SprintZero1.Factories;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class OpenPathWithBlockEvent : IRoomEvent
    {
        private bool _canTriggerEvent;
        private readonly IMovableEntity _movableBlock;
        private Vector2 _triggerPosition;
        private SoundEffect _sound;

        /// <summary>
        /// Create a new instance of the open door with block event
        /// </summary>
        /// <param name="room">The room where the event will be triggered</param>
        /// <param name="movableBlock">the block used to trigger the event</param>
        /// <param name="triggerPosition">the position the block needs to be in to trigger the event</param>
        public OpenPathWithBlockEvent(IMovableEntity movableBlock, Vector2 triggerPosition)
        {
            _canTriggerEvent = true;
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
                _canTriggerEvent = false;
                SoundFactory.PlaySound(SoundFactory.GetSound("secret"));
            }
        }
    }
}

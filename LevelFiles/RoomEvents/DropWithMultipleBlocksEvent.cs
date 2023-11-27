using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class DropWithMultipleBlocksEvent : IRoomEvent
    {
        private bool _canTriggerEvent;
        private readonly List<IMovableEntity> _movableBlocks;
        private readonly List<Vector2> _triggerPositions;
        private readonly int _requiredBlocks;
        private readonly TimeSpan _timeLimit;
        private TimeSpan _elapsedTime;
        
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="movableBlocks">List of movable blocks in level</param>
        /// <param name="triggerPositions">List of block destinations for event to trigger</param>
        /// <param name="requiredBlocks">Number of blocks needed to complete puzzle</param>
        /// <param name="timeLimit">Time limit for how fast to complete puzzle before penalty</param>
        public DropWithMultipleBlocksEvent(List<IMovableEntity> movableBlocks, List<Vector2> triggerPositions, int requiredBlocks, TimeSpan timeLimit)
        {
            _canTriggerEvent = true;
            _movableBlocks = movableBlocks;
            _triggerPositions = triggerPositions;
            _requiredBlocks = requiredBlocks;
            _timeLimit = timeLimit;
            _elapsedTime = TimeSpan.Zero;
        }
        
        /// <summary>
        /// On/Off for if the event can be triggered or not, if already triggered then false
        /// </summary>
        /// <returns>Boolean</returns>
        public bool CanTriggerEvent()
        {
            return _canTriggerEvent;
        }

        /// <summary>
        /// Trigger the event if everything is satisfied, in this case the blocks are moved in place
        /// </summary>
        public void TriggerEvent()
        {
            int blocksInPosition = 0;

            for (int i = 0; i < _movableBlocks.Count; i++)
            {
                if (_movableBlocks[i].Position == _triggerPositions[i])
                {
                    blocksInPosition++;
                }
            }

            if(blocksInPosition >= _requiredBlocks)
            {
                _canTriggerEvent = false;
                
            }
        }

        /// <summary>
        /// Updating the timer for keeping track of time allotted
        /// </summary>
        /// <param name="gameTime">Time in game</param>
        public void Update(TimeSpan gameTime)
        {
            _elapsedTime += gameTime;

            if(_elapsedTime > _timeLimit )
            {
                Penalty();
            }

        }

        /// <summary>
        /// Penalty for the player for not completing the puzzle in allotted time
        /// </summary>
        private void Penalty()
        {
            //spawn boss or lose health or something
        }
    }
}

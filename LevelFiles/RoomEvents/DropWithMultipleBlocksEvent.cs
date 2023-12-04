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
        private readonly DungeonRoom _room;
        private List<String> _doorToOpenDirections = new List<String>();
        private bool _canTriggerEvent;
        private readonly List<IMovableEntity> _movableBlocks;
        private readonly List<Vector2> _triggerPositions;
        private const int _requiredBlocks = 7;
        private const float TimeLimit = 30f;
        private float _elapsedTime;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="movableBlocks">List of movable blocks in level</param>
        /// <param name="triggerPositions">List of block destinations for event to trigger</param>
        /// <param name="requiredBlocks">Number of blocks needed to complete puzzle</param>
        public DropWithMultipleBlocksEvent(DungeonRoom room, List<IMovableEntity> movableBlocks, List<Vector2> triggerPositions, List<String> doorToOpenDirections)
        {
            _room = room;
            _canTriggerEvent = true;
            _movableBlocks = movableBlocks;
            _triggerPositions = triggerPositions;
            _doorToOpenDirections = doorToOpenDirections;
            _elapsedTime = 0f;
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
                ///drop minigun or open door or something
                ///puzzle complete
                
            }
        }

        /// <summary>
        /// Updating the timer for keeping track of time allotted
        /// </summary>
        /// <param name="gameTime">Time in game</param>
        public void Update(GameTime gameTime)
        {
            //each update will update elapsed time with time since last update
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //check if time limit was reached
            if(_elapsedTime > TimeLimit )
            {
                Penalty(); //Call penalty function
                _elapsedTime -= TimeLimit; //subtract time limit from elapsed time to accurately measure
            }

        }

        /// <summary>
        /// Penalty for the player for not completing the puzzle in allotted time
        /// </summary>
        private void Penalty()
        {
            //spawn boss or lose health or something
            //not implemented yet
        }
    }
}

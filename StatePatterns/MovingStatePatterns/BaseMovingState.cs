using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.MovingStatePatterns
{
    internal abstract class BaseEntityMovingState : IMovingEntityState
    {

        private Dictionary<Direction, Func<IMovingEntityState>> DirectionToStateMap; // invokes the specific state
        protected IMovableEntity entity;
        protected Vector2 Velocity;
        protected Vector2 position;
        /// <summary>
        /// Create the Direction to State map
        /// </summary>
        private void CreateDirectionToStateMap()
        {
            DirectionToStateMap = new Dictionary<Direction, Func<IMovingEntityState>>()
            {
                { Direction.North, () => new NorthMovingState(entity) },
                { Direction.South, () => new SouthMovingState(entity) },
                { Direction.West, () => new WestMovingState(entity) },
                { Direction.East, () => new EastMovingState(entity) }
            };
        }

        public BaseEntityMovingState()
        {
            CreateDirectionToStateMap();
        }

        public virtual void BePaused()
        {
            // pause not implemented yet
        }

        public virtual void ChangeDirection(Direction direction)
        {
            entity.State = DirectionToStateMap[direction].Invoke();
        }

        public abstract void Update(GameTime gameTime);
    }
}

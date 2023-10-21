using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;
using System.Collections.Generic;

namespace SprintZero1.StateMachines
{
    internal abstract class BaseEntityState : IEntityState
    {

        private Dictionary<Direction, Func<IEntityState>> DirectionToStateMap; // invokes the specific state 
        protected IMovableEntity entity;
        protected Vector2 Velocity;

        /// <summary>
        /// Create the Direction to State map
        /// </summary>
        private void CreateDirectionToStateMap()
        {
            DirectionToStateMap = new Dictionary<Direction, Func<IEntityState>>()
            {
                { Direction.North, () => new NorthMovingEntityState(this.entity) },
                { Direction.South, () => new SouthMovingEntityState(this.entity) },
                { Direction.West, () => new WestMovingEntityState(this.entity) },
                { Direction.East, () => new EastMovingEntityState(this.entity) }
            };
        }

        public BaseEntityState(IMovableEntity entity)
        {
            this.entity = entity;
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

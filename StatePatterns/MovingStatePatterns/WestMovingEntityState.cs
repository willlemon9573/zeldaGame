﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.MovingStatePatterns
{
    internal class WestMovingEntityState : BaseEntityMovingState
    {
        public WestMovingEntityState(IMovableEntity entity) : base()
        {
            this.entity = entity;
            Velocity = new Vector2(-1, 0);
        }

        public override void Update(GameTime gameTime)
        {
            entity.Move(Velocity);
        }
    }
}

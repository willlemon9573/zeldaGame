﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.MovingStatePatterns
{
    internal class SouthMovingState : BaseMovingState
    {
        public SouthMovingState(IMovableEntity entity) : base()
        {
            this.entity = entity;
            Velocity = new Vector2(0, 1);
        }

        public override void Update(GameTime gameTime)
        {
            entity.Move(Velocity);
        }
    }
}

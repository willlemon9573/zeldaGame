﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.MovingStatePatterns
{
    internal class EastMovingEntityState : BaseEntityMovingState
    {

        public EastMovingEntityState(IMovableEntity entity) : base()
        {
            this.entity = entity;
            Velocity = new Vector2(1, 0); // moving left
        }

        public override void Update(GameTime gameTime)
        {
            entity.Move(Velocity);
        }
    }
}

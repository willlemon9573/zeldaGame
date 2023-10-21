﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.MovingStatePatterns
{
    internal class IdleEntityState : BaseEntityMovingState
    {
        public IdleEntityState(IMovableEntity entity) : base()
        {
            this.entity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            // idle entity does not move so no need for this
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerAttackingState : BasePlayerState
    {

        public PlayerAttackingState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override void ChangeDirection(Direction newDirection)
        {
            // Left unimplemented. Consider using if we implement a means for link to change direction while attacking (Charging bow for example)
        }

        public override void Request()
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}


using System;
using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
	internal class PlayerAttackingState : BasePlayerState
	{
        public PlayerAttackingState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override void ChangeDirection(Direction newDirection)
        {
            throw new NotImplementedException();
        }

        public override void Request()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}


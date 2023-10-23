using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerIdleState : BasePlayerState
    {

        public PlayerIdleState(PlayerEntity playerEntity) : base(playerEntity)
        {
            //
        }

        public override void ChangeDirection(Direction newDirection)
        {
            //  Idle player does not change direction
        }

        public override void Request()
        {
            // Idle player does not handle a request
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}


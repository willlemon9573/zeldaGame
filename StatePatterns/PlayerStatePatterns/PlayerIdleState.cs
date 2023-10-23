using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerIdleState : BasePlayerState
    {
        /// <summary>
        /// Handles player when they are in the Idle State
        /// </summary>
        /// <param name="playerEntity">The player entering the idle state</param>
        public PlayerIdleState(PlayerEntity playerEntity) : base(playerEntity) { }

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
            // Idle player isn't updated
        }
    }
}


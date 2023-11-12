using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// Knockback the player 
    /// </summary>
	internal class PlayerKnockBackState : BasePlayerState
    {
        // Track time 
        private float _stateElapsedTime = 0f;
        private readonly float _timeToResetState = 1 / 7f;
        public PlayerKnockBackState(PlayerEntity playerEntity) : base(playerEntity)
        {
            //TODO: Implement logic
        }

        public override void ChangeDirection(Direction newDirection)
        {
            //TODO: Implement logic if needed
        }

        public override void Request()
        {
            //TODO: Implement the request handling logic
        }

        // Add override for Draw if needed

        public override void Update(GameTime gameTime)
        {
            //TODO: Implement Update logic
        }
    }
}


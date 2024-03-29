﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// Placeholder class until we implement interacting / pausing
    /// </summary>
    internal class PlayerPauseState : BasePlayerState
    {
        public PlayerPauseState(PlayerEntity playerEntity) : base(playerEntity)
        {
            //TODO: Implement logic here
        }

        public override void ChangeDirection(Direction newDirection)
        {
            //Player doesn't change direction when paused
        }

        public override void Request()
        {
            //TODO : Implement code to handle a request if paused
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Implement logic here if anything is updated during player pause
        }
    }
}


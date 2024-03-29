﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    internal interface IEnemyState
    {
        /// <summary>
        /// Handles transitioning to a new state
        /// </summary>
        /// <param name="newState">The new state to change to</param>
        void TransitionState(State newState);
        /// <summary>
        /// Handles directional state changes
        /// </summary>
        /// <param name="newDirection">The new direction of the player</param>
        void ChangeDirection(Direction newDirection);
        /// <summary>
        /// Handles updating the player
        /// </summary>
        /// <param name="gameTime">The current game time state</param>
        void Update(GameTime gameTime);
        /// </summary>
        /// <param name="spriteBatch">Sprite batch drawer</param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// Request the state the handle its state
        /// </summary>
        void Request();
        /// <summary>
        /// Block player state transitioning
        /// </summary>
        void BlockTransition();
        /// <summary>
        /// Unblock the player state transitioning
        /// </summary>
        void UnblockTranstion();
    }
}


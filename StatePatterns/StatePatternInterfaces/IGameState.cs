using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using System;

namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    internal interface IGameState
    {

        /// <summary>
        /// Add the players to the given state
        /// </summary>
        /// <param name="player">The player controller tuple</param>
        void AddPlayer(Tuple<IEntity, IController> player);

        /// <summary>
        /// Gets the desired player entity
        /// </summary>
        /// <param name="playerNumber">The number representing which player to get</param>
        /// <returns>The desired player entity</returns>
        IEntity GetPlayer(int playerNumber);

        /// <summary>
        /// Change the state of the game
        /// </summary>
        /// <param name="newState">The new state the game is changing to</param>
        void ChangeGameState(GameState newState);

        /// <summary>
        /// Handle the changes required when changing the state
        /// </summary>
        void Handle();

        /// <summary>
        /// Draw the current state information
        /// </summary>
        /// <param name="spriteBatch">The sprite batch for drawing</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Update the game during the state
        /// </summary>
        /// <param name="gameTime">The current game time state</param>
        void Update(GameTime gameTime);
    }
}

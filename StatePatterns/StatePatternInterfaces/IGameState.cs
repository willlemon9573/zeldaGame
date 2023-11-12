using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Enums;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    public interface IGameState
    {
        /// <summary>
        /// Returns the EntityManager of the State
        /// </summary>
        public EntityManager EntityManager { get; }

        /// <summary>
        /// List of all Controllers
        /// </summary>
        public List<IController> Controllers { get; }

        /// <summary>
        /// Change the state of the game
        /// </summary>
        /// <param name="newState">The new state the game is changing to</param>
        void ChangeGameState(GameState newState);

        /// <summary>
        /// Add a controller to the GameState
        /// </summary>
        /// <param name="controller"></param>
        public void AddController(IController controller);

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

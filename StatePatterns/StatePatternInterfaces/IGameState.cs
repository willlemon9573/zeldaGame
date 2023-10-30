using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    public interface IGameState
    {
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
    /// <summary>
    /// Interface for game state menus.
    /// This interface defines the basic functionality for game state menus, including methods for drawing and updating.
    /// </summary>
    /// <author>Zihe Wang</author>
    public interface IGameStateMenu
    {
        /// <summary>
        /// Draw the game state menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Update the game state menu.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for the update.</param>
        void Update(GameTime gameTime);
    }
}

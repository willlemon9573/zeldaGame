using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public interface ISprite
    {

        /// <summary>
        /// Draw the specified texture onto the game
        /// </summary>
        /// <param name="spriteBatch">Helper for drawing sprites in batches</param>
        /// <param name="texture">The texture to be drawn</param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// Update the specifi sprite based on gameTIme
        /// </summary>
        /// <param name="gameTime">The snapshot of the current game time/param>
        void Update(GameTime gameTime);
    }
}

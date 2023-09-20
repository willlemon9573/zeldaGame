using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public interface ISprite
    {
        /// <summary>
        /// Indicates if the sprite is in an attack state.
        /// </summary>
        bool attack { get; set; }

        /// <summary>
        /// Indicates the direction the sprite is facing.
        /// </summary>
        int direction { get; set; }

        /// <summary>
        /// Represents the location of the sprite.
        /// </summary>
        Vector2 location { get; set; }

        /// <summary>
        /// Draws the sprite onto the game.
        /// </summary>
        /// <param name="spriteBatch">Helper for drawing sprites in batches.</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the sprite based on the game time.
        /// </summary>
        /// <param name="gameTime">The snapshot of the current game time.</param>
        void Update(GameTime gameTime);
    }
}

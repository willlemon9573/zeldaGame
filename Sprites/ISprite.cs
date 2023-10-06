using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public interface ISprite
    {
        /// <summary>
        /// Draws the sprite at the given position. Allows for sprite effects, scaling and rotation
        /// </summary>
        /// <param name="spriteBatch">Tool for handling drawing all sprites</param>
        /// <param name="position">the position for the sprite to be draw</param>
        /// <param name="spriteEffects">Horizontal or vertical rotation(Optional)</param>
        /// <param name="rotation">The angle in radians to rate the sprite around the origin(Optional)</param>
        /// <param name="scale">The uniform multiple to WINDOW_SCALE the sprite width and height(Optional)</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float scale = 1f, float rotation = 0f);

        public void Update(GameTime gameTime);
    }
}

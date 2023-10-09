using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public interface ISprite
    {
        /// <summary>
        /// Draws the _sprite at the given position. Allows for _sprite effects, scaling and rotation
        /// </summary>
        /// <param name="spriteBatch">Tool for handling drawing all sprites</param>
        /// <param name="position">the position for the _sprite to be draw</param>
        /// <param name="spriteEffects">Horizontal or vertical rotation(Optional)</param>
        /// <param name="rotation">The angle in radians to rate the _sprite around the origin(Optional)</param>
        /// <param name="scale">The uniform multiple to WINDOW_SCALE the _sprite width and height(Optional)</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f);

        public void Update(GameTime gameTime);
    }
}

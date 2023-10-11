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
        /// <param name="layerDepth">The layer depth the sprite should be drawn on</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0f);

        public void Update(GameTime gameTime);
    }
}

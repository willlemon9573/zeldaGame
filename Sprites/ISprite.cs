using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public interface ISprite
    {
        public int Width { get; }
        public int Height { get; }
        /// <summary>
        /// Draws the _sprite at the given position. Allows for _sprite effects, scaling and rotation
        /// </summary>
        /// <param name="spriteBatch">Tool for handling drawing all sprites</param>
        /// <param name="position">the position for the _sprite to be draw</param>
        /// <param name="spriteEffects">Used for flipping a sprite Horizontally or Vertically based on entity Direction(Optional)</param>
        /// <param name="rotation">The angle in radians to rate the _sprite around the origin(Optional)</param>
        /// <param name="layerDepth">The layer depth the sprite should be drawn on</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0f);
        /// <summary>
        /// Handles updating sprite information (primarily used for animating sprites)
        /// </summary>
        /// <param name="gameTime">Holds the state of the game</param>
        public void Update(GameTime gameTime);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    /// <summary>
    /// Controls drawing of non-animated sprites
    /// @Author Aaron Heishman
    /// </summary>
    public class NonAnimatedSprite : ISprite
    {
        private readonly Rectangle _sourceRectangle;
        private readonly Texture2D _spriteSheet;

        public NonAnimatedSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {
            _sourceRectangle = sourceRectangle;
            _spriteSheet = spriteSheet;
            //_spriteSheet = Texture2DManager.GetTileSheet();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0.5f)
        {
            int width = _sourceRectangle.Width;
            int height = _sourceRectangle.Height;
            /* the center of origin for rotation */
            Vector2 origin = new(width / 2, height / 2);
            /* 
             * this overload of draw requires a color mask. Color.White maintains the original sprite color. This can be used to apply a 'tint' to the sprite if desired
             * May want to add a functionality to interface to allow changing color of the sprite for things like entities taking damage 
             */
            Color colorMask = Color.White;
            Rectangle destinationRectangle = new((int)position.X, (int)position.Y, width, height);
            spriteBatch.Draw(_spriteSheet, destinationRectangle, _sourceRectangle, colorMask, rotation, origin, spriteEffects, layerDepth);
        }

        public void Update(GameTime gameTime)
        {
            // non animated sprites do not get animated
        }
    }
}
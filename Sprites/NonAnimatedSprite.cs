using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public class NonAnimatedSprite : ISprite
    {
        private readonly Rectangle _sourceRectangle;
        private readonly Texture2D _spriteSheet;


        public NonAnimatedSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {
            _sourceRectangle = sourceRectangle;
            _spriteSheet = spriteSheet;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects, float rotation = 0f, float layerDepth = 0f)
        {
            int width = _sourceRectangle.Width;
            int height = _sourceRectangle.Height;
            /* the center of origin for rotation */
            Vector2 origin = new(width / 2, height / 2);
            Rectangle destinationRectangle = new((int)position.X, (int)position.Y, width, height);
            spriteBatch.Draw(_spriteSheet, destinationRectangle, _sourceRectangle, Color.White, rotation, origin, spriteEffects, layerDepth);
        }

        public void Update(GameTime gameTime)
        {
            // non animated sprites do not get animated
        }
    }
}

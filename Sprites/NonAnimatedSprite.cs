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
        private int _spriteWidth;
        private int _spriteHeight;

        public int Width { get => _spriteWidth; }

        public int Height { get => _spriteHeight; }

        public NonAnimatedSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {
            _spriteWidth = sourceRectangle.Width;
            _spriteHeight = sourceRectangle.Height;
            _sourceRectangle = sourceRectangle;
            _spriteSheet = spriteSheet;
            //_spriteSheet = Texture2DManager.GetTileSheet();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0.5f)
        {
            /* the center of origin for rotation */
            Vector2 origin = new(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2);
            Rectangle destinationRectangle = new((int)position.X, (int)position.Y, _spriteWidth, _spriteHeight);
            spriteBatch.Draw(_spriteSheet, destinationRectangle, _sourceRectangle, color, rotation, origin, spriteEffects, layerDepth);
        }

        public void Update(GameTime gameTime)
        {
            // non animated sprites do not get animated
        }
    }
}
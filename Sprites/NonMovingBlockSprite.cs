
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public class NonMovingBlockSprite : ISprite
    {

        private Rectangle sourceRectangle; // source of rectangle to pull from the block sprite sheet
        private Vector2 location; // the location to display the block
        private readonly Texture2D spriteSheet; // the source file to draw from

        public Vector2 Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        /// <summary>
        /// NonMovingBlockSprite constructor
        /// </summary>
        /// <param name="sourceRectangle">The position on the Sprite Sheet to pull the sprites from</param>
        /// <param name="spriteSheet">The specific sprite sheet containing the blocks</param>
        /// <param name="location">The location to draw the sprite</param>
        public NonMovingBlockSprite(Rectangle sourceRectangle, Texture2D spriteSheet, Vector2 location)
        {
            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            this.location = location;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // non-moving block sprite does not update or move
        }
    }
}

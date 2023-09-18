
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public class NonMovingBlockSprite : ISprite
    {

        private Rectangle sourceRectangle; // source of rectangle to pull from the block sprite sheet
        private Vector2 location; // the location to display the block
        private readonly Texture2D spriteSheet; // the source file to draw from

        /// <summary>
        /// NonMovingBlockSprite constructor
        /// </summary>
        /// <param name="sourceRectangle">The position on the spriteSheet to pull the sprites from</param>
        /// <param name="spriteSheet">The specific sprite sheet containing the blocks</param>
        public NonMovingBlockSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {
            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            location = new Vector2(200, 230); // start with the tile in the middle of the screen
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            // non-moving block sprite does not update or move
        }
    }
}

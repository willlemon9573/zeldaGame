
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1.Sprites
{
    public class CreateMovingLinkSprite : ISprite
    {

        private readonly List<Rectangle> sourceRectangle;
        private readonly Vector2 location; // the location to display the block
        private readonly Texture2D spriteSheet; // the source file to draw from

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMovingLinkSprite"/> class.
        /// </summary>
        /// <param name="spritePositions">The sprite positions.</param>
        /// <param name="linkSpriteSheet">The link sprite sheet.</param>
        /// <param name="position">The position.</param>
        public CreateMovingLinkSprite(List<Rectangle> spritePositions, Texture2D LinkSpriteSheet, Vector2 position)
        {

            this.sourceRectangle = spritePositions;
            this.spriteSheet = LinkSpriteSheet;
            this.location = position; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle1 = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            Rectangle destinationRectangle2 = new Rectangle((int)location.X+ 49, (int)location.Y, 49, 49);
            Rectangle destinationRectangle3 = new Rectangle((int)location.X+ 49 + 49, (int)location.Y, 49, 49);
            Rectangle destinationRectangle4 = new Rectangle((int)location.X+ 49 + 49 + 49, (int)location.Y, 49, 49);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle1, sourceRectangle[0], Color.White);
            spriteBatch.Draw(spriteSheet, destinationRectangle2, sourceRectangle[1], Color.White);
            spriteBatch.Draw(spriteSheet, destinationRectangle3, sourceRectangle[2], Color.White);
            spriteBatch.Draw(spriteSheet, destinationRectangle4, sourceRectangle[3], Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        { 

        }
    }
}

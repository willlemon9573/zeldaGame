
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1.Sprites
{
    public class CreateMovingLinkSprite : ISprite
    {
        private readonly List<Rectangle> sourceRectangles;  // Storing all the rectangles for animation
        private readonly Vector2 location;
        private readonly Texture2D spriteSheet;

        // To track which frame/rectangle to use
        private int currentFrameIndex;

        public CreateMovingLinkSprite(List<Rectangle> spritePositions, Texture2D LinkSpriteSheet, Vector2 position, int frameIndex)
        {
            this.sourceRectangles = spritePositions;
            this.spriteSheet = LinkSpriteSheet;
            this.location = position;
            this.currentFrameIndex = frameIndex;  // Set the starting frame
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle1 = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle1, sourceRectangles[currentFrameIndex], Color.White);
            spriteBatch.End();
        }

        // If you want to implement some kind of animation in the future, 
        // you can increase the currentFrameIndex here
        public void Update(GameTime gameTime)
        {
            // example: currentFrameIndex = (currentFrameIndex + 1) % sourceRectangles.Count;
        }
    }

}


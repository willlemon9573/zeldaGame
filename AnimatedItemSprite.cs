using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Sprites
{
    public class AnimatedItemSprite : ISprite
    {

        public  Texture2D spriteSheet { get; set; }
        private Rectangle sourceRectangle;
        private Vector2 location;
        private readonly Texture2D priteSheet;
        private int currentFrame;
        private int totalFrames;



        public AnimatedItemSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {

            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            location = new Vector2(600, 130); 
            currentFrame = 0;
            totalFrames = 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = spriteSheet.Width / 2;
            int height = spriteSheet.Height;
            int row = 1;
            int column = 2;

            Rectangle newRectangle = new Rectangle((int)(sourceRectangle.X + currentFrame),sourceRectangle.Y, 16, 16 );
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle, newRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            currentFrame += 17;
            if (currentFrame >= 17) {
                currentFrame = 0;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data.Common;

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
        private float timeElapsed, timeToUpdate;



        public AnimatedItemSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {

            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            location = new Vector2(600, 130);
            currentFrame = 0;
            totalFrames = 2;
            timeToUpdate = 1f / 10;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = spriteSheet.Width / 2;
            int height = spriteSheet.Height;
            
            
            Rectangle newRectangle = new Rectangle(((int)(sourceRectangle.X))+(17*currentFrame),sourceRectangle.Y, 16, 16 );
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle, newRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate) {
                timeElapsed -= timeToUpdate;
                //timeToUpdate = 1f / 2;
                currentFrame++;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }
           // timeToUpdate = 60;

           
           
        }
    }
}

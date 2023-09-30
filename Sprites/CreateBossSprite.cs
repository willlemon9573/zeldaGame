using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SprintZero1.Sprites
{
    public class CreateBossSprite : ISprite
    {
        private List<Rectangle> sourceRectangle;
        private Vector2 location;
        private readonly Texture2D spriteSheet;
        private double timeElapsed, timeToUpdate;
        private int currentFrameIndex;
        private int totalFrames;

        public Vector2 Position {
            get { return location; }
            set { location = value; }
        }

        public CreateBossSprite(List<Rectangle> sourceRectangle, Texture2D spriteSheet, Vector2 location, int frameIndex) 
        {
            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            this.location = location;
            this.currentFrameIndex = frameIndex;
            totalFrames = 2;
            timeToUpdate = 1f / 1;
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 150, 150);
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle[currentFrameIndex], Color.White);
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (double)gameTime.ElapsedGameTime.TotalSeconds;
            if(timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                //timeToUpdate = 1f / 2
                currentFrameIndex++;

                if(currentFrameIndex >= totalFrames)
                {
                    currentFrameIndex = 0;
                }
            }
            //timeToUpdate = 60
        }
    }
}

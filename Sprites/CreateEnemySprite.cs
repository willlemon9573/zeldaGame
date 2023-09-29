using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Sprites
{
    public class CreateEnemySprite : ISprite
    {
        private List<Rectangle> sourceRectangles;
        private Vector2 location;
        private readonly Texture2D spriteSheet;
        private double timeElapsed, timeToUpdate;
        private int currentFrame;
        private int totalFrames;
        




        public CreateEnemySprite(List<Rectangle> sourceRectangles, Texture2D spriteSheet, Vector2 location)
        {
            this.sourceRectangles = sourceRectangles;
            this.spriteSheet = spriteSheet;
            this.location = location;
            currentFrame = 0;
            totalFrames = 2;
            timeToUpdate = 1f / 10;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangles, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (double)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                //timeToUpdate = 1f / 2
                currentFrame++;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }
            //timeToUpdate = 60
        }
    }
}

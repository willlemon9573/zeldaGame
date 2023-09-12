
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1
{
    public class MovingInPlaceSoraAnimated : ISprite
    {
        /// <summary>
        /// Private members of ISprite
        /// </summary>
        private readonly int rows, columns, width, height, totalFrames;
        private int currentFrame;
        private Vector2 location;
        private float timeElapsed, timeToUpdate;

        /// <summary>
        /// Sets the time to update to the next frame
        /// </summary>
        private int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }


        /// <summary>
        /// Update the frames based on how much time has passed since the last frame has been updated
        /// </summary>
        /// <param name="timeInSeconds">snapshot of game time in seconds</param>
        private void UpdateFrames(float timeInSeconds)
        {
            timeElapsed += timeInSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                currentFrame++;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 1;
                }
            }
        }
        
        /// <summary>
        /// Construct an object of a non moving animated sora
        /// </summary>
        public MovingInPlaceSoraAnimated()
        {
            rows = 1;
            columns = 8;
            totalFrames = rows * columns;
            width = 45;
            height = 77;
            location = new Vector2(400, 240);
            FramesPerSecond = 10;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        { 
            int currCol = currentFrame % columns;
            Rectangle sourceRectangle = new(width * currCol, height, width, height);
            Rectangle destinationRectangle = new((int)location.X, (int)location.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateFrames(deltaTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1
{
    public class UpMovingSprite : ISprite
    {
        private readonly int rows, columns, width, height, totalFrames;
        private int currentFrame, speed;
        private double timeElapsed, timeToUpdate;
        private Vector2 location; // Implementing the Location property from the interface
        private bool moveOnce = true;


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
        private void UpdateFrames(double timeInSeconds)
        {
            
            timeElapsed += timeInSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                currentFrame++;
                if (currentFrame > totalFrames)
                {
                    currentFrame = 1;
                }
            }
        }

        /// <summary>
        /// Handles moving the sprite. When the sprite reaches a specific boundary, change directions
        /// </summary>
        /// <param name="timeInSeconds">Snapshot of game time in seconds</param>
        private void MoveSprite(float timeInSeconds)
        {
            if (moveOnce)
            {
                location.Y += speed * timeInSeconds;
                if (location.Y <= boundaries[0] || location.Y >= boundaries[1])
                {
                    speed *= -1;
                }
                moveOnce = false;
            }
            }

        public UpMovingSprite()
        {
            rows = 1;
            columns = 8;
            totalFrames = rows * columns;
            width = 45;
            height = 77;
            boundaries = new int[] { 300, 500 };
            FramesPerSecond = 10;
            speed = 50;
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
            MoveSprite(deltaTime);
        }
        public int Direction { get; set; } = 1;
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
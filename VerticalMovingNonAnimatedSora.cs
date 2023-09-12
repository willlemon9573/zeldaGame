using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1
{
    internal class VerticalMovingNonAnimatedSora : ISprite
    {
        private Vector2 location;
        private readonly int x_start, y_start, width, height;
        private readonly int[] boundaries;
        private int speed;

        /// <summary>
        /// Handles moving the sprite. When the sprite reaches a specific boundary, change directions
        /// </summary>
        /// <param name="deltaTime">elpased game time in seconds</param>
        private void MoveSprite(float deltaTime)
        {
            location.Y += speed * deltaTime;
            if (location.Y <= boundaries[0] || location.Y >= boundaries[1])
            {
                speed *= -1;
            }
        }

        /// <summary>
        /// Construct an object of an animated Sora moving horizontally
        /// </summary>
        public VerticalMovingNonAnimatedSora()
        {
            location = new Vector2(400, 240);
            x_start = 0; 
            y_start = 0;
            width = 46;
            height = 78;
            speed = 50;
            boundaries = new int[] { 180, 300 };
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        { 
            Rectangle sourceRectangle = new Rectangle(x_start, y_start, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MoveSprite(deltaTime);
        }
    }
}

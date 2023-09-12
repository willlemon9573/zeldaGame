using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1
{
    public class CreditsSprite : ISprite
    {
        private Vector2 text_position;
        private readonly int width, height, x_start, y_start;

        /// <summary>
        /// Constructs the credits sprite object
        /// </summary>
        public CreditsSprite()
        {
            text_position = new Vector2(250, 350);
            width = 391;
            height = 118;
            x_start = 5;
            y_start = 212;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Rectangle sourceRectangle = new Rectangle(x_start, y_start, width, height);
            Rectangle destinationRectangle = new Rectangle((int)text_position.X, (int)text_position.Y, width, height);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        void ISprite.Update(GameTime gameTime)
        {
           // unimplemented - text sprite does not update
        }
    }
}

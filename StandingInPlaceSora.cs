
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1
{
    public class StandingInPlaceSora : ISprite
    {
        private Vector2 location;
        private readonly int x_start, y_start, width, height;

        /// <summary>
        /// Construct the Standing In Place Sora Sprite Object
        /// </summary>
        public StandingInPlaceSora()
        {
            location = new Vector2(400, 240);
            x_start = 0;
            y_start = 0;
            width = 46;
            height = 78;
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
            // sprite not updated - unused
        }
    }
}

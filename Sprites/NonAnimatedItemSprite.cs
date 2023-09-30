
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/* this class will be refractored into a single NonAnimatedSprite class */
namespace SprintZero1.Sprites
{
    public class NonAnimatedItemSprite : ISprite
    {

        private Rectangle sourceRectangle; 
        private Vector2 location; 
        private readonly Texture2D spriteSheet; 

        public NonAnimatedItemSprite(Rectangle sourceRectangle, Texture2D spriteSheet)
        {
            this.sourceRectangle = sourceRectangle;
            this.spriteSheet = spriteSheet;
            location = new Vector2(600, 130);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
           
        }
    }
}
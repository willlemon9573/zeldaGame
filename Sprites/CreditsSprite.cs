using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SprintZero1.Sprites
{
    public class CreditsSprite : ISprite
    {
        private Vector2 text_position;
        private readonly int width, height, x_start, y_start;
        /* Unused, but saved in case we want to use it */

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
        public CreditsSprite(Vector2 location, Texture2D spritesheet)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        void ISprite.Update(GameTime gameTime)
        {
            // unimplemented - text sprite does not update
        }
    }
}

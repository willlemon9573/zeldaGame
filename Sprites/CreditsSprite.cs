using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1.Sprites
{
    public class CreditsSprite : ISprite
    {
        /* Unused, but saved in case we want to use it */

        /// <summary>
        /// Constructs the credits sprite object
        /// </summary>
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Sprites
{
    internal class CharacterSprite : ISprite
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            // Not Needed
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Color color, Rectangle source, Rectangle destination) 
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, source, destination, color);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            // Not Needed
        }
    }
}

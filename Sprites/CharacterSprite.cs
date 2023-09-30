using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Will be used in the future, but not used currently */
namespace SprintZero1.Sprites
{
    internal class CharacterSprite : ISprite
    {
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

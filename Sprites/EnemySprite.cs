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
    internal class EnemySprite : ISprite
    {
        Vector2 pos;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Rectangle destinationRectangle;
        EnemyFactory factory;

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,  }

        public void Update(GameTime gameTime)
        {
            destinationRectangle = new Rectangle(, );
            Draw(spriteBatch);
        }
    }
}

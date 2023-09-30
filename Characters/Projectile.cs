using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Characters
{
    internal class Projectile : ICharacter
    {
        public Vector2 pos;
        SpriteBatch spriteBatch;
        Rectangle source = new Rectangle(238, 154, 8, 16);
        Rectangle destination = new Rectangle(0,0,20,40);
        Texture2D texture;
        

        public Projectile(SpriteBatch spriteBatch, Vector2 startPos, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.pos = startPos;
            this.texture = texture;

        }

        public void Update(GameTime timer)
        {
            pos.X -= 6;
            destination.X = (int) pos.X;
            destination.Y = (int) pos.Y;    
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destination, source, Color.White);
            spriteBatch.End();
        }

        
    }
}

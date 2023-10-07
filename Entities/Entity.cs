using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal abstract class Entity : IEntity
    {
        public ISprite sprite;
        public Vector2 pos;
        Vector2 IEntity.Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Entity(ISprite sprite, Vector2 pos) 
        {
            this.sprite = sprite;
            this.pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            // Update Logic 
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, pos);
        }
    }
}

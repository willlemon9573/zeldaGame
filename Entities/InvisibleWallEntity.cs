using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Entities
{
    internal class InvisibleWallEntity : IEntity
    {
        public Vector2 Position { get { return _position; } set { _position = value; } }
        private Vector2 _position;
        ICollider _collider;

        public InvisibleWallEntity(Vector2 position, Vector2 dimensions)
        {
            _position = position;
            _collider = new LevelBlockCollider(this, new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // N/A
        }

        public void Update(GameTime gameTime)
        {
            // N/A
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class InvisibleWallEntity : IEntity, ICollidableEntity
    {
        public Vector2 Position { get { return _position; } set { _position = value; } }
        private Vector2 _position;
        private ISprite _sprite;
        ICollider _collider;
        public ICollider Collider { get { return _collider; } }


        //192long 112high
        public InvisibleWallEntity(ISprite sprite, Vector2 position, Vector2 dimensions)
        {
            _sprite = sprite;
            _position = position;
            _collider = new LevelBlockCollider(new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y));
            ColliderManager.AddStaticCollider(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // N/A
        }

        public void Update(GameTime gameTime)
        {
            // N/A
        }

        ~InvisibleWallEntity()
        {
            ColliderManager.RemoveCollider(this);
        }
    }
}

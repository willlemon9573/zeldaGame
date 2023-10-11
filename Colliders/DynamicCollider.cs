using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal abstract class DynamicCollider : ICollider
    {
        Rectangle _collider;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        IEntity _parent;
        public IEntity Parent { get { return _parent; } set { _parent = value; } }

        public DynamicCollider(IEntity parent, Rectangle _collider)
        {
            _parent = parent;
            this._collider = _collider;
            AddCollider();
        }
        public void AddCollider()
        {
            ColliderManager.AddDynamicCollider(this);
        }

        public void OnCollision(IEntity collidedEntity, ICollider collidedCollider)
        {
            // Add Stuff
        }

        public void RemoveCollider()
        {
            ColliderManager.RemoveCollider(this);
        }

        public void Update(GameTime gameTime)
        {
            _collider.X = (int)Parent.Position.X;
            _collider.Y = (int)Parent.Position.Y;
        }
    }
}

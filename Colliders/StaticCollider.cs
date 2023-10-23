using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal abstract class StaticCollider : ICollider
    {
        Rectangle _collider;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        IEntity _parent;
        public IEntity Parent { get { return _parent; } set { _parent = value; } }

        public int Delta { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public StaticCollider(IEntity entity, Rectangle _collider)
        {
            this.Collider = _collider;
            this._parent = entity;
            AddCollider();
        }

        public void AddCollider()
        {
            ColliderManager.AddStaticCollider(this);
        }

        public void OnCollision(IEntity collidedEntity, ICollider collidedCollider)
        {
            //TODO Implement what happens on collision
        }

        public void RemoveCollider()
        {
            ColliderManager.RemoveCollider(this);
        }

        public void Update(GameTime gameTime)
        {
            // Static- no need to update
        }
    }
}

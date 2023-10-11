using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Entities;
using System.Diagnostics;

namespace SprintZero1.Colliders
{
    internal abstract class StaticCollider : ICollider
    {
        Rectangle _collider;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        IEntity _parent;
        public IEntity Parent { get { return _parent; } set { _parent = value; } }

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
            Debug.WriteLine("Collision Detected!");
            if (collidedEntity is PlayerEntity)
            {
                PriorityQueue<Vector2, float> colliderDistances = new PriorityQueue<Vector2, float>();
                colliderDistances.Enqueue(new Vector2(0, -1), Vector2.Distance(new Vector2(collidedCollider.Collider.Center.X, collidedCollider.Collider.Center.Y), new Vector2(_collider.Left + (_collider.Width / 2), _collider.Top)));
                colliderDistances.Enqueue(new Vector2(0, 1), Vector2.Distance(new Vector2(collidedCollider.Collider.Center.X, collidedCollider.Collider.Center.Y), new Vector2(_collider.Left + (_collider.Width / 2), _collider.Bottom)));
                colliderDistances.Enqueue(new Vector2(-1, 0), Vector2.Distance(new Vector2(collidedCollider.Collider.Center.X, collidedCollider.Collider.Center.Y), new Vector2(_collider.Left, _collider.Top + (_collider.Height / 2))));
                colliderDistances.Enqueue(new Vector2(1, 0), Vector2.Distance(new Vector2(collidedCollider.Collider.Center.X, collidedCollider.Collider.Center.Y), new Vector2(_collider.Right, _collider.Top + (_collider.Height / 2))));

                Vector2 pos = collidedEntity.Position + colliderDistances.Dequeue();
                // Insert Pushback Code Here
                collidedEntity.Position = pos;
            }
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

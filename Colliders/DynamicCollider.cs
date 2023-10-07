using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            _collider.X = (int) Parent.Position.X;
            _collider.Y = (int) Parent.Position.Y;
        }
    }
}

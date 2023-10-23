﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System.ComponentModel;

namespace SprintZero1.Colliders
{
    internal abstract class DynamicCollider : ICollider
    {
        Rectangle _collider;

        public int Delta { get { return _delta; } set { _delta = value; } }
        private int _delta;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        IEntity _parent;
        public IEntity Parent { get { return _parent; } set { _parent = value; } }

        /// <summary>
        /// DynamicCollider Constructor
        /// </summary>
        /// <param name="parent">The Parent Entity</param>
        /// <param name="_collider">The Collider Rectangle</param>
        /// <param name="delta">The size offset</param>
        public DynamicCollider(IEntity parent, Rectangle _collider, int delta = 0)
        {
            _parent = parent;
            this._collider = new Rectangle(_collider.X - delta, _collider.Y - delta, _collider.Width + delta, _collider.Height + delta);
            Delta = delta;
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

        /// <summary>
        /// Updates Collider to center point based on delta
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            _collider.X = (int)Parent.Position.X - _delta;
            _collider.Y = (int)Parent.Position.Y - _delta;
        }
    }
}
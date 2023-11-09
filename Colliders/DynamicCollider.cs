using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal class DynamicCollider : ICollider
    {
        Rectangle _collider;

        public int Delta { get { return _delta; } set { _delta = value; } }
        private int _delta;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        /// <summary>
        /// DynamicCollider Constructor
        /// </summary>
        /// <param name="parent">The Parent Entity</param>
        /// <param name="_collider">The Collider Rectangle</param>
        /// <param name="delta">The size offset</param>
        public DynamicCollider(Rectangle _collider, int delta = 0)
        {
            this._collider = new Rectangle(_collider.X - delta - (_collider.Width / 2), _collider.Y - delta - (_collider.Height / 2), _collider.Width + delta, _collider.Height + delta);
            Delta = delta;
        }

        /// <summary>
        /// Updates Collider to center point based on delta
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(IEntity parent)
        {
            _collider.X = (int)parent.Position.X - (((Collider.Width / 2) - Delta)/2) - 1 ;
            _collider.Y = (int)parent.Position.Y - (((Collider.Height / 2) - Delta) / 2) - 1;
        }
    }
}

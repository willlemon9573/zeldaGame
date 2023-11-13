using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal class StaticCollider : ICollider
    {
        Rectangle _collider;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        IEntity _parent;
        public IEntity Parent { get { return _parent; } set { _parent = value; } }
        public int Delta { get { return _delta; } set { _delta = value; } }
        private int _delta;

        public StaticCollider(Rectangle _collider, int delta = 0)
        {
            this._collider = new Rectangle(_collider.X - delta - (_collider.Width / 2), _collider.Y - delta - (_collider.Height / 2), _collider.Width + delta, _collider.Height + delta);
            this.Delta = delta;
        }

        public void Update(IEntity parent)
        {
            _collider.X = (int)parent.Position.X - _delta - (_collider.Width / 2);
            _collider.Y = (int)parent.Position.Y - _delta - (_collider.Height / 2);
        }
    }
}

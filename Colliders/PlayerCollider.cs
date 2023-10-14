using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal class PlayerCollider : DynamicCollider
    {
        public PlayerCollider(IEntity parent, Rectangle _collider, int delta = 0) : base(parent, _collider, delta)
        {
            this.Parent = parent;
            this.Collider = new Rectangle(_collider.X - delta, _collider.Y - delta, _collider.Width + delta, _collider.Height + delta);
            this.Delta = delta;
            AddCollider();
        }
    }
}

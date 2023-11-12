using Microsoft.Xna.Framework;

namespace SprintZero1.Colliders
{
    internal class PlayerCollider : DynamicCollider
    {
        public PlayerCollider(Rectangle _collider, int delta = 0) : base(_collider, delta)
        {
            this.Collider = new Rectangle(_collider.X - delta - (_collider.Width / 2), _collider.Y - delta - (_collider.Height / 2), _collider.Width + (delta * 2), _collider.Height + (delta * 2));
            this.Delta = delta;
        }
    }
}

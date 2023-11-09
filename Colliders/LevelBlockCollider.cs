using Microsoft.Xna.Framework;

namespace SprintZero1.Colliders
{
    internal class LevelBlockCollider : StaticCollider
    {
        /// <summary>
        /// Constructor for LevelBlockCollider
        /// </summary>
        /// <param name="entity">Parent Entity</param>
        /// <param name="_collider">Collider Rectangle</param>
        public LevelBlockCollider(Rectangle _collider, int delta = 0) : base(_collider, delta)
        {
            this.Collider = new Rectangle(_collider.X - delta - (_collider.Width / 2), _collider.Y - delta - (_collider.Height / 2), _collider.Width + delta, _collider.Height + delta);
            this.Delta = delta;
        }
    }
}

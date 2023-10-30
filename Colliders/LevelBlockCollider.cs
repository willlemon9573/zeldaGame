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
            this.Collider = _collider;
            this.Delta = delta;
        }
    }
}

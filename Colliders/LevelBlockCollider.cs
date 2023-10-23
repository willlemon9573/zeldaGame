using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal class LevelBlockCollider : StaticCollider
    {
        /// <summary>
        /// Constructor for LevelBlockCollider
        /// </summary>
        /// <param name="entity">Parent Entity</param>
        /// <param name="_collider">Collider Rectangle</param>
        public LevelBlockCollider(IEntity entity, Rectangle _collider) : base(entity, _collider)
        {
            this.Parent = entity;
            this.Collider = _collider;
        }
    }
}

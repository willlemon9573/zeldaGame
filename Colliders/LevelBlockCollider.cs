using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.Colliders
{
    internal class LevelBlockCollider : StaticCollider
    {
        public LevelBlockCollider(IEntity entity, Rectangle _collider) : base(entity, _collider)
        {
            this.Parent = entity;
            this.Collider = _collider;
        }
    }
}

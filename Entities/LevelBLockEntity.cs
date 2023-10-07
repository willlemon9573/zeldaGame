using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class LevelBLockEntity : Entity
    {
        StaticCollider collider;
        public LevelBLockEntity(ISprite sprite, Vector2 pos, bool collidable) : base(sprite, pos)
        {
            this._sprite = sprite;
            this._position = pos;

            if (collidable)
                collider = new LevelBlockCollider(this, new Rectangle((int)pos.X, (int)pos.Y, 16, 16));
        }
    }
}

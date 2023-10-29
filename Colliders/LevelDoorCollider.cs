using Microsoft.Xna.Framework;

namespace SprintZero1.Colliders
{
    internal class LevelDoorCollider : StaticCollider
    {
        public LevelDoorCollider(Rectangle _collider) : base(_collider)
        {
            this.Collider = _collider;
        }
    }
}

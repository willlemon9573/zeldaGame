using Microsoft.Xna.Framework;

namespace SprintZero1.Colliders
{
    internal class OpenDoorCollider : StaticCollider
    {
        public OpenDoorCollider(Rectangle _collider) : base(_collider)
        {
            this.Collider = _collider;
        }
    }
}

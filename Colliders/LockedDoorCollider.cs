using Microsoft.Xna.Framework;

namespace SprintZero1.Colliders
{
    internal class LockedDoorCollider : StaticCollider
    {
        /// <summary>
        /// This offset is used for match the width/height to the locked door so the player doesn't walk "into" the door when trying to unlock it
        /// </summary>
        private readonly int offset = 16;
        public LockedDoorCollider(Rectangle _collider, int delta = 0) : base(_collider, delta)
        {
            // Updating collider to prevent user from walking "into" the door while trying to unlock it
            Rectangle updatedCollider = new Rectangle(_collider.X, _collider.Y, _collider.Width + offset, _collider.Height + offset);
            this.Collider = updatedCollider;
        }
    }
}

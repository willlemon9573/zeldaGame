using Microsoft.Xna.Framework;
using System.Drawing;

namespace SprintZero1.Colliders.EntityColliders
{
    internal class WallCollider : StaticCollider
    {
        // wrapper for walls. using to make sure object can't fly outside the wall
        public WallCollider(Vector2 position, Size dimensions, float scaleFactor = 1, int offsetX = 0, int offsetY = 0) : base(position, dimensions, scaleFactor, offsetX, offsetY)
        {
        }
    }
}

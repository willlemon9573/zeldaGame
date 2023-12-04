using Microsoft.Xna.Framework;
using System.Drawing;

namespace SprintZero1.Colliders.ItemColliders
{
    internal class TriforceCollider : StaticCollider
    {
        public TriforceCollider(Vector2 position, Size dimensions, float scaleFactor = 1, int offsetX = 0, int offsetY = 0) : base(position, dimensions, scaleFactor, offsetX, offsetY)
        {
        }
    }
}

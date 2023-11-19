﻿using Microsoft.Xna.Framework;
using System.Drawing;

namespace SprintZero1.Colliders.DoorColliders
{
    internal class BreakableWallCollider : StaticCollider
    {
        public BreakableWallCollider(Vector2 position, Size dimensions, float scaleFactor = 1, int offsetX = 0, int offsetY = 0) : base(position, dimensions, scaleFactor, offsetX, offsetY)
        {
        }
    }
}

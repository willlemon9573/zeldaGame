﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System.Diagnostics;

namespace SprintZero1.Colliders
{
    internal class PlayerCollider : DynamicCollider
    {
        public PlayerCollider(IEntity parent, Rectangle _collider) : base(parent, _collider)
        {
            this.Collider = Collider;
        }
    }
}

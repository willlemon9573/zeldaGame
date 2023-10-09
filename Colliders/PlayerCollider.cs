using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

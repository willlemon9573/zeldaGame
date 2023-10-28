using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Entities
{
    internal class LevelBLockEntity : Entity
    {
        StaticCollider collider;
        public LevelBLockEntity(ISprite sprite, Vector2 pos, bool collidable) : base(sprite, pos)
        {
            this.sprite = sprite;
            this.pos = pos;

            if(collidable)
                collider = new LevelBlockCollider(this, new Rectangle((int) pos.X, (int) pos.Y, 16, 16));
        }
    }
}

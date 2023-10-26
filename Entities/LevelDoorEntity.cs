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
    internal class LevelDoorEntity : BackgroundSpriteEntity, ICollidableEntity 
    {
        public int _nextLevel;
        readonly LevelDoorCollider _collider;

        public ICollider Collider { get { return _collider; } }

        public LevelDoorEntity(ISprite sprite, Vector2 position, int nextlevel) : base(sprite, position)
        {
            this._sprite = sprite;
            this._position = position;
            _nextLevel = nextlevel;
            _collider = new LevelDoorCollider(this, new Rectangle((int)position.X, (int)position.Y, 16, 16), _nextLevel);
        }
    }
}

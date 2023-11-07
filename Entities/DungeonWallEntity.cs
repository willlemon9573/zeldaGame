using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class DungeonWallEntity : BackgroundSpriteEntity, ICollidableEntity
    {
        private StaticCollider _wallCollider;
        public ICollider Collider { get { return _wallCollider; } }
        /// <summary>
        /// Create a new wall entity
        /// </summary>
        /// <param name="sprite">The sprite the wall entity will use</param>
        /// <param name="position">The position to draw the wall entity</param>
        /// <param name="dimensions">The specific dimensions of the wall entity for collision</param>
        public DungeonWallEntity(ISprite sprite, Vector2 position, Rectangle dimensions) : base(sprite, position)
        {
            this._sprite = sprite;
            this._position = position;
            _wallCollider = new LevelBlockCollider(dimensions);
        }
    }
}

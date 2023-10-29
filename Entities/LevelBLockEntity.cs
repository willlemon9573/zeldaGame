using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class LevelBlockEntity : BackgroundSpriteEntity, ICollidableEntity
    {
        StaticCollider collider;

        public ICollider Collider { get { return collider; } }

        /// <summary>
        /// Constructor for simple level tile
        /// </summary>
        /// <param name="sprite">Block ISprite</param>
        /// <param name="pos">Block position</param>
        /// <param name="collidable">Whether or not block is Collidable</param>
        public LevelBlockEntity(ISprite sprite, Vector2 pos) : base(sprite, pos)
        {
            this._sprite = sprite;
            this._position = pos;

            collider = new LevelBlockCollider(new Rectangle((int)pos.X, (int)pos.Y, 16, 16));
        }


    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class LevelBlockEntity : BackgroundSpriteEntity
    {
        StaticCollider collider;

        /// <summary>
        /// Constructor for simple level tile
        /// </summary>
        /// <param name="sprite">Block ISprite</param>
        /// <param name="pos">Block position</param>
        /// <param name="collidable">Whether or not block is Collidable</param>
        public LevelBlockEntity(ISprite sprite, Vector2 pos, bool collidable) : base(sprite, pos)
        {
            this._sprite = sprite;
            this._position = pos;

            if (collidable)
                collider = new LevelBlockCollider(this, new Rectangle((int)pos.X, (int)pos.Y, 16, 16));
        }
    }
}

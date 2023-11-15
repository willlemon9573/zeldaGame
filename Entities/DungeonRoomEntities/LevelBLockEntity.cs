using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class LevelBlockEntity : BackgroundSpriteEntity, ICollidableEntity
    {
        StaticCollider collider;
        public ICollider Collider { get { return collider; } }
        private const float LAYER_DEPTH = 0.3f;
        /// <summary>
        /// Constructor for simple level tile
        /// </summary>
        /// <param name="sprite">Block ISprite</param>
        /// <param name="pos">Block position</param>
        public LevelBlockEntity(ISprite sprite, Vector2 pos) : base(sprite, pos)
        {
            _sprite = sprite;
            _position = pos;
            float scaleFactor = 0.9f;
            collider = new PushBackCollider(_position, new System.Drawing.Size(sprite.Width, sprite.Height), scaleFactor);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position, SpriteEffects.None, 0, LAYER_DEPTH);
        }
    }
}

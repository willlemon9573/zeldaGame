using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class LevelBlockEntity : ICollidableEntity
    {
        protected const float _scaleFactor = 0.9f;
        protected ICollider _collider;
        protected ISprite _sprite;
        protected Vector2 _position;
        protected const float LAYER_DEPTH = 0.3f;
        public ICollider Collider { get { return _collider; } }

        public Vector2 Position { get { return _position; } set { _position = value; } }

        /// <summary>
        /// Constructor for simple level tile
        /// </summary>
        /// <param name="sprite">Block ISprite</param>
        /// <param name="pos">Block position</param>
        public LevelBlockEntity(ISprite sprite, Vector2 pos)
        {
            this._sprite = sprite;
            this._position = pos;
            this._collider = new PushBackCollider(_position, new System.Drawing.Size(sprite.Width, sprite.Height), _scaleFactor);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this._sprite.Draw(spriteBatch, _position, SpriteEffects.None, 0, LAYER_DEPTH);
        }

        public virtual void Update(GameTime gameTime)
        {
            this._collider.Update(this);
        }
    }
}

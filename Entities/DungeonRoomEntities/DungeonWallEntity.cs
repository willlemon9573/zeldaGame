using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;
using Size = System.Drawing.Size;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class DungeonWallEntity : ICollidableEntity
    {
        private readonly StaticCollider _wallCollider;
        private Vector2 _entityPosition;
        private readonly ISprite _entitySprite;
        private readonly SpriteEffects _spriteEffects = SpriteEffects.None;
        private readonly float _spriteRotation = 0;
        private readonly float _layerDepth = 0.4f;
        public ICollider Collider { get { return _wallCollider; } }

        public Vector2 Position { get { return _entityPosition; } set { _entityPosition = value; } }

        /// <summary>
        /// Create a new wall entity
        /// </summary>
        /// <param name="sprite">The sprite the wall entity will use</param>
        /// <param name="position">The position to draw the wall entity</param>
        /// <param name="dimensions">The specific dimensions of the wall entity for collision</param>
        public DungeonWallEntity(ISprite sprite, Vector2 position)
        {

            _wallCollider = new PushBackCollider(_entityPosition, new Size(sprite.Width, sprite.Height));
            _entitySprite = sprite;
            _entityPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            _entitySprite.Update(gameTime);
            _wallCollider.Update(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _entitySprite.Draw(spriteBatch, _entityPosition, _spriteEffects, _spriteRotation, _layerDepth);
        }
    }
}

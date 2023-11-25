using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    /// <summary>
    /// Entity for the fire trap in one of the secret rooms
    /// </summary>
    internal class FireTrapEntity : ICollidableEntity
    {
        private StaticCollider _trapCollider;
        private readonly ISprite _entitySprite;
        private Vector2 _entityPosition;
        private readonly SpriteEffects _spriteEffects = SpriteEffects.None;
        private readonly float _spriteRotation = 0;
        private readonly float _layerDepth = 0.4f;
        /// <summary>
        /// Get the the collider for the entity
        /// </summary>
        public ICollider Collider { get { return _trapCollider; } }
        /// <summary>
        /// Get set the entity position
        /// </summary>
        public Vector2 Position
        {
            get { return _entityPosition; }
            set { _entityPosition = value; }
        }
        /// <summary>
        /// Create a new entity that is a fire trap entity
        /// </summary>
        /// <param name="sprite">The sprite for the entity</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="dimensions">The dimensions of the entity</param>
        public FireTrapEntity(ISprite sprite, Vector2 position, Rectangle dimensions)
        {
            _entitySprite = sprite;
            _entityPosition = position;
            _trapCollider = new PushBackCollider(_entityPosition, new System.Drawing.Size(dimensions.Width, dimensions.Height));
        }
        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="spriteBatch">The sprite batch that the entity will be drawn in</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _entitySprite.Draw(spriteBatch, Position, Color.White, _spriteEffects, _spriteRotation, _layerDepth);
        }
        /// <summary>
        /// Updates the entity's animation and collider
        /// </summary>
        /// <param name="gameTime">The current state of the game time</param>
        public void Update(GameTime gameTime)
        {
            _entitySprite.Update(gameTime);
            _trapCollider.Update(this);
        }
    }
}

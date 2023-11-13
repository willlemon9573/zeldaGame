using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using Size = System.Drawing.Size;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    /// <summary>
    /// Base abstract implemention for each door.
    /// Note that due to how collisions work I am forced to create a new type of door
    /// for each collider, but each derived class will be used as a different way to recognize
    /// collision
    /// </summary>
    internal abstract class BaseDoorEntity : ICollidableEntity
    {
        /* The default dimensions for each door in the game */
        protected readonly Size DoorDimensions = new Size(16, 16);
        /* fields */
        protected ICollider _doorCollider;
        protected Vector2 _doorPosition;
        protected ISprite _doorSprite;
        protected string _doorDestination;
        protected Direction _doorDirection;
        protected readonly int offset = 16; /* used for offsetting the doors that aren't open */
        private readonly SpriteEffects SpriteEffect = SpriteEffects.None;
        private readonly float rotation = 0f;
        private readonly float layerDepth = 0.5f;
        /// <summary>
        /// Get the collider
        /// </summary>
        public ICollider Collider { get { return _doorCollider; } }
        /// <summary>
        /// Get and set the position of the door
        /// </summary>
        public Vector2 Position { get { return _doorPosition; } set { _doorPosition = value; } }
        /// <summary>
        /// Get the destination for the new destination the door will lead to
        /// </summary>
        public string DoorDestination { get { return _doorDestination; } }
        /// <summary>
        /// Get the door direction for replacing a door
        /// </summary>
        public Direction DoorDirection { get { return _doorDirection; } }
        /// <summary>
        /// Constructor for a new door entity
        /// </summary>
        /// <param name="entitySprite">The sprite of the door</param>
        /// <param name="position">The position the door will be drawn at</param>
        /// <param name="destination">The destination where the door leads</param>
        /// <param name="direction">The direction that the door is placed</param>
        protected BaseDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction)
        {
            _doorPosition = position;
            _doorSprite = entitySprite;
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width, DoorDimensions.Height);
            _doorCollider = new LockedDoorCollider(colliderDimensions);
            _doorDirection = direction;
            _doorDestination = destination;
        }
        /// <summary>
        /// Draws the door's sprite and any other values
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _doorSprite.Draw(spriteBatch, _doorPosition, SpriteEffect, rotation, layerDepth);
        }
        /// <summary>
        /// Updates the entity collider
        /// </summary>
        /// <param name="gameTime">The current state of the game time</param>
        public void Update(GameTime gameTime)
        {
            _doorCollider.Update(this);
        }
    }
}

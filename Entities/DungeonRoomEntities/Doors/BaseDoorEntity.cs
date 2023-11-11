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

        protected BaseDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction)
        {
            this._doorPosition = position;
            this._doorSprite = entitySprite;
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width, DoorDimensions.Height);
            this._doorCollider = new StaticCollider(colliderDimensions);
            this._doorDirection = direction;
            this._doorDestination = destination;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _doorSprite.Draw(spriteBatch, _doorPosition);
        }

        public void Update(GameTime gameTime)
        {
            _doorCollider.Update(this);
        }
    }
}

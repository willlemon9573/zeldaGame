using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    /// <summary>
    /// Base abstract implemention for each door.
    /// Note that due to how collisions work I am forced to create a new type of door
    /// for each collider, but each derived class will be used as a different way to recognize
    /// collision
    /// </summary>
    internal abstract class BaseDoorEntity : IDoorEntity
    {


        protected const float ScaleFactor = 0.9f; // the scale factor to lower the size of the door collider to prevent unwanted collisions
        /* Offset for the positions of colliders */
        private const int NorthSouthOffsetX = 0;
        private const int NorthSouthOffsetY = 5;
        private const int EastWestOfffsetX = 5;
        private const int EastWestOfffsetY = 0;
        /// <summary>
        /// Offset dictionary for colliders to move collider further back to prevent early room movement
        /// </summary>
        protected readonly Dictionary<Direction, Vector2> _colliderOffsetDictionary = new Dictionary<Direction, Vector2>()
        {
            { Direction.North, new Vector2(NorthSouthOffsetX, -NorthSouthOffsetY) },
            { Direction.South, new Vector2(NorthSouthOffsetX, NorthSouthOffsetY) },
            { Direction.East, new Vector2(EastWestOfffsetX, EastWestOfffsetY) },
            { Direction.West, new Vector2(-EastWestOfffsetX, EastWestOfffsetY) },
        };

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
        protected Rectangle _colliderDimmensions;
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
            this._doorPosition = position;
            this._doorSprite = entitySprite;
            this._doorDirection = direction;
            this._doorDestination = destination;
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
            this._doorCollider.Update(this);
        }

        /// <summary>
        /// Handles opening the door
        /// </summary>
        public virtual void OpenDoor()
        {
            /* base door does not open, set to virtual so all derived classes do not have to implement */
        }
    }
}

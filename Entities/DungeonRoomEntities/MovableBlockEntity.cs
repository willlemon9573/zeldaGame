using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System.Collections.Generic;
using Size = System.Drawing.Size;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class MovableBlockEntity : IMovableEntity, ICollidableEntity
    {
        private const float MovementSpeed = 5f;
        private readonly Size BlockDimensions = new Size(16, 16);
        private readonly ISprite _blockSprite;
        private ICollider _collder;
        private Vector2 _currentPosition;
        private readonly Vector2 _maxPosition;
        Rectangle _colliderDimensions;
        Dictionary<Direction, Vector2> _movemenetDirectionDict;
        Direction _directionToMove;
        /// <summary>
        /// Default direction for the these types of blocks unless we find a use for them
        /// </summary>
        public Direction Direction { get => Direction.North; set { } }
        public Vector2 Position { get { return _currentPosition; } set { _currentPosition = value; } }
        /// <summary>
        /// Create the dictionary that dictates how quickly the entity will move when being pushed
        /// </summary>
        private void CreateMovementDictionary()
        {
            _movemenetDirectionDict = new Dictionary<Direction, Vector2>() {
                {Direction.North, new Vector2(0, -MovementSpeed) },
                {Direction.South, new Vector2(0, MovementSpeed) },
                {Direction.East, new Vector2(MovementSpeed, 0) },
                {Direction.West, new Vector2(-MovementSpeed, 0) }
           };
        }
        /// <summary>
        /// Constructs an instance of a movable block entity
        /// </summary>
        /// <param name="blockSprite">The entity's sprite</param>
        /// <param name="startingPosition">The entity's starting position</param>
        /// <param name="MaxPosition">The entity's final position</param>
        /// <param name="directionToMove">The direction in which the entity can move</param>
        public MovableBlockEntity(ISprite blockSprite, Vector2 startingPosition, Vector2 MaxPosition, Direction directionToMove)
        {
            _blockSprite = blockSprite;
            _currentPosition = startingPosition;
            _maxPosition = MaxPosition;
            _colliderDimensions = new Rectangle((int)startingPosition.X, (int)startingPosition.Y, BlockDimensions.Width, BlockDimensions.Height);
            _collder = new MovableBlockCollider(_currentPosition, new System.Drawing.Size(blockSprite.Width, blockSprite.Height));
            _directionToMove = directionToMove;
            CreateMovementDictionary();
        }

        public ICollider Collider { get => _collder; }

        public void ChangeDirection(Direction newDirection)
        {
            /* unused */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _blockSprite.Draw(spriteBatch, _currentPosition);
        }

        public void Move()
        {
            if (_collder is not MovableBlockCollider) { return; }

            if (_currentPosition != _maxPosition)
            {
                _currentPosition += _movemenetDirectionDict[_directionToMove];
            }
            else
            {
                _collder = new PushBackCollider(_currentPosition, new System.Drawing.Size(_blockSprite.Width, _blockSprite.Height));
            }
        }

        public void Update(GameTime gameTime)
        {
            _collder.Update(this);
        }
    }
}

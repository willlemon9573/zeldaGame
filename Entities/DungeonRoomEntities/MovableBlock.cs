using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class MovableBlock : LevelBlockEntity, ICollidableEntity, IMovableEntity
    {

        private readonly Dictionary<Direction, Vector2> _movementDict;
        private Direction _movementDirection;
        private Vector2 _maxPosition;
        public Direction Direction { get { return _movementDirection; } set { _movementDirection = value; } }

        private readonly DungeonRoom _room;


        /// <summary>
        /// Create a movable block entity
        /// </summary>
        /// <param name="sprite">The entity sprite</param>
        /// <param name="pos">the starting position for the entity</param>
        /// <param name="maxPos">the max position the entity can move</param>
        /// <param name="room">the room that the block belongs to</param>
        /// <param name="direction"></param>
        public MovableBlock(ISprite sprite, Vector2 pos, Vector2 maxPos, Direction direction) : base(sprite, pos)
        {
            _movementDict = new Dictionary<Direction, Vector2>()
            {
                {Direction.North, new Vector2(0, -1f)},
                {Direction.South, new Vector2(0, 1f)},
                {Direction.West, new Vector2(-1f, 0)},
                {Direction.East, new Vector2(1f, 0)},
            };
            this._movementDirection = direction;
            this._position = pos;
            this._collider = new MovableBlockCollider(pos, new System.Drawing.Size(sprite.Width, sprite.Height), _scaleFactor);
            this._sprite = sprite;
            this._maxPosition = maxPos;
        }

        /// <summary>
        /// Change the direction that the block can move
        /// </summary>
        /// <param name="newDirection"></param>
        public void ChangeDirection(Direction newDirection)
        {
            this._movementDirection = newDirection;
        }

        /// <summary>
        /// move the block in its current direction
        /// </summary>
        public void Move()
        {
            this._position += _movementDict[this._movementDirection];
            if (this._position == _maxPosition)
            {
                _collider = new PushBackCollider(this._position, new System.Drawing.Size(_sprite.Width, _sprite.Height), _scaleFactor);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            this._collider.Update(this);
        }
    }
}

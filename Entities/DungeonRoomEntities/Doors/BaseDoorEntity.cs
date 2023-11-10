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
    /// </summary>
    internal abstract class BaseDoorEntity : IDoorEntity
    {
        /* The default dimensions for each door in the game */
        protected readonly Size DoorDimensions = new Size(16, 16);
        /* fields */
        protected ICollider _doorCollider;
        protected Vector2 _doorPosition;
        protected ISprite _doorSprite;
        protected Point _doorDestination;
        protected Direction _doorDirectionLocation;
        public ICollider Collider { get { return _doorCollider; } }

        public Vector2 Position { get { return _doorPosition; } set { _doorPosition = value; } }

        protected BaseDoorEntity(ISprite entitySprite, Vector2 position, Point destination, Direction direction)
        {
            this._doorPosition = position;
            this._doorSprite = entitySprite;
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width, DoorDimensions.Height);
            this._doorCollider = new StaticCollider(colliderDimensions);
            this._doorDirectionLocation = direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _doorSprite.Draw(spriteBatch, _doorPosition);
        }

        public void Update(GameTime gameTime)
        {
            _doorCollider.Update(this);
        }

        public abstract void Open();
    }
}

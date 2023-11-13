using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class OpenDoorEntity : BaseDoorEntity
    {
        public OpenDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width, DoorDimensions.Height);
            this._doorCollider = new OpenDoorCollider(colliderDimensions);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._doorSprite.Draw(spriteBatch, this._doorPosition);
        }
    }
}

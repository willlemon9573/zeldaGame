using Microsoft.Xna.Framework;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class StairEntity : BaseDoorEntity
    {

        public StairEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            /* Stairs are smaller than doors, so the collider needs to be smaller */
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width - offset, DoorDimensions.Height - offset);
            this._doorCollider = new OpenDoorCollider(colliderDimensions);
        }
    }
}

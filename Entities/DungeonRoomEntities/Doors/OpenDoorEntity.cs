using Microsoft.Xna.Framework;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class OpenDoorEntity : BaseDoorEntity
    {

        public OpenDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            float scaleFactor = 0.9f;
            Vector2 offset = _colliderOffsetDictionary[direction];
            this._doorCollider = new OpenDoorCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height), scaleFactor, (int)offset.X, (int)offset.Y);
        }
    }
}

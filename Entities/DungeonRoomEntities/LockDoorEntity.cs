using Microsoft.Xna.Framework;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities
{
    internal class LockDoorEntity : BaseDoorEntity
    {
        public LockDoorEntity(ISprite entitySprite, Vector2 position, Point destination, Direction direction) : base(entitySprite, position, destination, direction)
        {

        }

        public override void Open()
        {

        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class OpenDoorEntity : BaseDoorEntity
    {
        public OpenDoorEntity(ISprite entitySprite, Vector2 position, Point destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            /* Opened doors uses the base class */
        }

        public override void Open()
        {
            /* opened doors are opened by default */
        }
    }
}

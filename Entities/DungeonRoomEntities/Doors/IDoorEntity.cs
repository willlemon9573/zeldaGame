using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal interface IDoorEntity : ICollidableEntity
    {
        /// <summary>
        /// Get the destination for the new destination the door will lead to
        /// </summary>
        public string DoorDestination { get; }

        /// <summary>
        /// Get the door direction for replacing a door
        /// </summary>
        public Direction DoorDirection { get; }

        public void OpenDoor();
    }
}

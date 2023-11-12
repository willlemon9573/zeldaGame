using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using System.Diagnostics;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextLevelCommand : ICommand
    {
        private string _destination;
        public EnterNextLevelCommand(ICollidableEntity playerEntity, ICollidableEntity doorEntity)
        {
            _destination = (doorEntity as OpenDoorEntity).DoorDestination;
        }

        public void Execute()
        {
            Debug.WriteLine(_destination);
        }
    }
}

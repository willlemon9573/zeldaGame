using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextRoomCommand : ICommand
    {
        private string _destination;
        ICollidableEntity _playerEntity;
        public EnterNextRoomCommand(ICollidableEntity playerEntity, ICollidableEntity doorEntity)
        {
            _destination = (doorEntity as BaseDoorEntity).DoorDestination;
            _playerEntity = playerEntity;
        }

        public void Execute()
        {

        }
    }
}

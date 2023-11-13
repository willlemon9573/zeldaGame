using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextRoomCommand : ICommand
    {
        private readonly string _destination;
        private readonly ICollidableEntity _playerEntity;
        public EnterNextRoomCommand(ICollidableEntity playerEntity, ICollidableEntity doorEntity)
        {
            _destination = (doorEntity as BaseDoorEntity).DoorDestination;
            _playerEntity = playerEntity;
        }

        public void Execute()
        {
            (GameStatesManager.GetGameState(Enums.GameState.Playing) as GamePlayingState).LoadDungeonRoom(_destination, Enums.Direction.North);
        }
    }
}

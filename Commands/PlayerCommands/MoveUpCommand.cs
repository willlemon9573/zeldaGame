using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class MoveUpCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveUpCommand(IMovableEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = entity;
        }

        public void Execute()
        {
            if (_movableEntity.Direction != Direction.North)
            {
                _movableEntity.ChangeDirection(Direction.North);
            }
            _movableEntity.Move();
        }
    }
}

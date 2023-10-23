using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveRightCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveRightCommand(IMovableEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = entity;
        }

        public void Execute()
        {
           if (_movableEntity.Direction != Direction.East)
            {
                _movableEntity.ChangeDirection(Direction.East);
            }
            _movableEntity.Move();
        }
    }
}

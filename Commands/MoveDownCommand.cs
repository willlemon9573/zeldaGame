using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveDownCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveDownCommand(IMovableEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = entity;
        }

        public void Execute()
        {
            if (_movableEntity.Direction != Direction.South)
            {
                _movableEntity.ChangeDirection(Direction.South);
            }
            _movableEntity.Move();
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveUpCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveUpCommand(IEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = (IMovableEntity)entity;
        }

        public void Execute()
        {
            _movableEntity.ChangeDirection(Direction.North);
        }
    }
}

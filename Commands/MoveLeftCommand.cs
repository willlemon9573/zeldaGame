using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveLeftCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveLeftCommand(IEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = (IMovableEntity)entity;
        }

        public void Execute()
        {
            _movableEntity.ChangeDirection(Direction.West);
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.StatePatterns.MovingStatePatterns;

namespace SprintZero1.Commands
{
    internal class IdleCommand : ICommand
    {
        IMovableEntity _movableEntity;

        public IdleCommand(IEntity entity)
        {
            _movableEntity = (IMovableEntity)entity;
        }
        public void Execute()
        {
            _movableEntity.State = new IdleEntityState(_movableEntity);
        }
    }
}

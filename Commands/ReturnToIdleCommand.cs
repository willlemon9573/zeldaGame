using SprintZero1.Entities;
using SprintZero1.Enums;
namespace SprintZero1.Commands
{

    internal class ReturnToIdleCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public ReturnToIdleCommand(IEntity entity)
        {
            _movableEntity = (IMovableEntity)entity;
        }

        public void Execute()
        {
            if (_movableEntity.State == State.Attacking)
            {
                return;
            }
            _movableEntity.State = State.Idle;
        }
    }
}

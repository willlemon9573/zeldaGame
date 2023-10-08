using SprintZero1.Entities;
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

        }
    }
}

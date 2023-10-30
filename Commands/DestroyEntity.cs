using SprintZero1.Entities;
using SprintZero1.Managers;

namespace SprintZero1.Commands
{
    internal class DestroyEntity : ICommand
    {
        IEntity deadEntityWalking;

        public DestroyEntity(IEntity entityToLive, IEntity entityToDIE)
        {
            deadEntityWalking = entityToDIE;
            Execute();
        }

        public void Execute()
        {
            EntityManager.Remove(deadEntityWalking);
        }
    }
}

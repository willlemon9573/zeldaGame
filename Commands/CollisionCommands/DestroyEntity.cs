using SprintZero1.Entities;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class DestroyEntity : ICommand
    {
        IEntity deadEntityWalking;

        public DestroyEntity(IEntity entityToLive, IEntity entityToDIE)
        {
            deadEntityWalking = entityToDIE;
        }

        public void Execute()
        {
            // Replace with kill entity code
        }
    }
}

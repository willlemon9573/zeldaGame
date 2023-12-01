using SprintZero1.Entities;
using SprintZero1.Managers;

namespace SprintZero1.Commands
{
    public class DecrementHealthCommand : ICommand
    {
        IEntity _player;
        public DecrementHealthCommand(IEntity player) {
            _player = player;
        }
        public void Execute()
        {
            float h = 0.5f;
            HUDManager.DecrementHealth(_player, h);
        }
    }
}

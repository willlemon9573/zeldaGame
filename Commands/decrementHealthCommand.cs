using SprintZero1.Managers;

namespace SprintZero1.Commands
{
    public class DecrementHealthCommand : ICommand
    {
        public DecrementHealthCommand() { }
        public void Execute()
        {
            float h = 0.5f;
            HUDManager.DecrementHealth(h);
        }
    }
}

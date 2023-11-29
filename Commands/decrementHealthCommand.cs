using SprintZero1.Managers;

namespace SprintZero1.Commands
{
    public class DecrementHealthCommand : ICommand
    {
        public DecrementHealthCommand() { }
        public void Execute()
        {
            float h = 4f;
            HUDManager.DecrementHealth(h);

        }
    }
}

using SprintZero1.Managers;

namespace SprintZero1.Commands
{
    internal class IncrementHealthCommand : ICommand
    {
        public IncrementHealthCommand() { }
        public void Execute()
        {
            float h = 1f;
            HUDManager.IncrementHearts(h, 4);

        }
    }
}

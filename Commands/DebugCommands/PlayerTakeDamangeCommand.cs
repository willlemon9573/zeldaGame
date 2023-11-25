using SprintZero1.Entities;

namespace SprintZero1.Commands.DebugCommands
{
    internal class PlayerTakeDamangeCommand : ICommand
    {
        readonly ICombatEntity _player;
        public PlayerTakeDamangeCommand(IEntity player)
        {
            _player = player as ICombatEntity;
        }

        public void Execute()
        {
            _player.TakeDamage(1f);
        }
    }
}

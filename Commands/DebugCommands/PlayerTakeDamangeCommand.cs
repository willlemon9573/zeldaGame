using SprintZero1.Entities.EntityInterfaces;

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
            _player.TakeDamage(0.5f);
        }
    }
}

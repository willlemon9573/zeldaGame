using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PlayerEnemyTouchDamageCommand : ICommand
    {
        ICombatEntity _player;
        private const float DefaultDamage = 0.5f;
        public PlayerEnemyTouchDamageCommand(IEntity player)
        {
            _player = player as ICombatEntity;
        }
        public void Execute()
        {

            _player.TakeDamage(DefaultDamage);
        }
    }
}

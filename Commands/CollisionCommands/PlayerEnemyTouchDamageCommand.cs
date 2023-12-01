using SprintZero1.Entities;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PlayerEnemyTouchDamageCommand : ICommand
    {
        ICombatEntity _player;
        ICombatEntity _enemy;
        public PlayerEnemyTouchDamageCommand(IEntity player, IEntity enemy)
        {
            _player = player as ICombatEntity;
            _enemy = enemy as ICombatEntity;
        }
        public void Execute()
        {
            float temporaryDamage = 1f;
            _player.TakeDamage(temporaryDamage);
        }
    }
}

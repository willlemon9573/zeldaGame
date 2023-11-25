using SprintZero1.Entities;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class SwordEnemyCollisionCommand : ICommand
    {
        private IWeaponEntity _weapon;
        private ICombatEntity _enemy;
        public SwordEnemyCollisionCommand(ICollidableEntity sword, ICollidableEntity enemy)
        {
            _weapon = sword as IWeaponEntity;
            _enemy = enemy as ICombatEntity;
        }
        public void Execute()
        {
            _enemy.TakeDamage(1f);
        }
    }
}

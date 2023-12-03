using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class SwordEnemyCollisionCommand : ICommand
    {
        private readonly float _weaponDamage;
        private readonly ICombatEntity _enemy;
        public SwordEnemyCollisionCommand(ICollidableEntity sword, ICollidableEntity enemy)
        {
            _weaponDamage = (sword as IWeaponEntity).WeaponDamage;
            _enemy = enemy as ICombatEntity;
        }
        public void Execute()
        {
            _enemy.TakeDamage(_weaponDamage);
        }
    }
}

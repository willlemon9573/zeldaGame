using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnemyBoomerangDamageCommand : ICommand
    {

        private readonly ICollidableEntity _projectile;
        private readonly ICombatEntity _combatEntity;
        public EnemyBoomerangDamageCommand(ICollidableEntity projectile, ICollidableEntity combatEntity)
        {
            _projectile = projectile;
            _combatEntity = combatEntity as ICombatEntity;
        }
        public void Execute()
        {
            if (_projectile is BoomerangBasedEntity ReturningProjectile)
            {
                _combatEntity.TakeDamage(ReturningProjectile.WeaponDamage);
            }
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Entities.BowAndMagicFireEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class ProjectileDamageCommand : ICommand
    {

        private readonly ICollidableEntity _projectile;
        private readonly ICombatEntity _combatEntity;
        public ProjectileDamageCommand(ICollidableEntity projectile, ICollidableEntity combatEntity)
        {
            _projectile = projectile;
            _combatEntity = combatEntity as ICombatEntity;
        }
        public void Execute()
        {
            if (_projectile is NonComingBackWeaponEntity nonReturningProjectile)
            {
                _combatEntity.TakeDamage(nonReturningProjectile.WeaponDamage);
                nonReturningProjectile.Stop();
            }
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Entities.BoomerangEntity;
using SprintZero1.Entities.BowAndMagicFireEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class ProjectileDamageCommand : ICommand
    {
        private const float temp_damage = 1f;
        private readonly ICollidableEntity _projectile;
        private readonly ICombatEntity _combatEntity;
        public ProjectileDamageCommand(ICollidableEntity projectile, ICollidableEntity combatEntity)
        {
            _projectile = projectile;
            _combatEntity = combatEntity as ICombatEntity;
        }
        public void Execute()
        {
            _combatEntity.TakeDamage(temp_damage);
            if (_projectile is NonComingBackWeaponEntity nonReturningProjectile)
            {
                nonReturningProjectile.Stop();
            }
            else if (_projectile is BoomerangBasedEntity returningProjectile)
            {
                returningProjectile.ReturnBoomerang();
            }
        }
    }
}

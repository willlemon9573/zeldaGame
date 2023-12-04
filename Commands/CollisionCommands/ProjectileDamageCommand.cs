using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BowAndMagicFireEntity;

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
            if (_projectile is IWeaponEntity projectile)
            {
                _combatEntity.TakeDamage(projectile.WeaponDamage);
            }
            if (_projectile is NonComingBackWeaponEntity nonComingBackWeaponEntity)
            {
                nonComingBackWeaponEntity.Stop();
            }
        }
    }
}

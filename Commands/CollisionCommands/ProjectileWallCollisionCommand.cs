using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;
using SprintZero1.Entities.WeaponEntities.BowAndMagicFireEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class ProjectileWallCollisionCommand : ICommand
    {
        private readonly ICollidableEntity _projectile;


        public ProjectileWallCollisionCommand(ICollidableEntity projectile, ICollidableEntity wall)
        {
            _projectile = projectile;
        }
        public void Execute()
        {
            if (_projectile is BoomerangBasedEntity boomerang)
            {
                if (boomerang.CollidedWithObject) { return; }
                boomerang.CollidedWithObject = true;
                boomerang.ForceReturnBoomerang();

            }
            else if (_projectile is NonComingBackWeaponEntity projectile)
            {
                projectile.Stop();
            }

        }
    }
}

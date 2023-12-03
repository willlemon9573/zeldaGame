using SprintZero1.Entities;
using SprintZero1.Entities.BoomerangEntity;
using SprintZero1.Entities.BowAndMagicFireEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class ProjectileWallCollisionCommand : ICommand
    {
        private readonly ICollidableEntity _projectile;
        private readonly ICollidableEntity _wall;


        public ProjectileWallCollisionCommand(ICollidableEntity projectile, ICollidableEntity wall)
        {
            _projectile = projectile;
            _wall = wall;
        }
        public void Execute()
        {
            if (_projectile is BoomerangBasedEntity boomerang)
            {
                if (boomerang.CollidedWithObject) { return; }
                boomerang.CollidedWithObject = true;
                boomerang.ReturnBoomerang();

            }
            else if (_projectile is NonComingBackWeaponEntity projectile)
            {
                projectile.Stop();
            }

        }
    }
}

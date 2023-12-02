using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.BoomerangEntity;
using SprintZero1.Entities.BowAndMagicFireEntity;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class ProjectileWallCollisionCommand : ICommand
    {
        private readonly ICollidableEntity _projectile;
        private readonly ICollidableEntity _wall;

        private bool IsInWall()
        {
            Rectangle intersection = Rectangle.Intersect(_projectile.Collider.Collider, _wall.Collider.Collider);
            Vector2 projectilePosition = _projectile.Position;
            if (intersection.Contains((int)projectilePosition.X, (int)projectilePosition.Y) && _projectile is BoomerangBasedEntity boomerang)
            {
                boomerang.StopProjectile();
                (GameStatesManager.CurrentState as GamePlayingState).RemoveProjectile(_projectile);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProjectileWallCollisionCommand(ICollidableEntity projectile, ICollidableEntity wall)
        {
            _projectile = projectile;
            _wall = wall;
            _pushback = new PushBackCommand(projectile, wall);
        }
        public void Execute()
        {
            if (_projectile is BoomerangBasedEntity boomerang)
            {
                if (IsInWall()) { return; }
                boomerang.ReturnBoomerang();
            }
            else if (_projectile is NonComingBackWeaponEntity projectile)
            {
                projectile.Stop();
            }

        }
    }
}

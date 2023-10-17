using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{

    internal class bombProjectile : IProjectile
    {
        private double timer = 0;
        private double waitingTime = 600;
        ProjectileEntity _projectile;

        public bombProjectile(ProjectileEntity ProjectileEntity)
        {
            _projectile = ProjectileEntity;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= waitingTime)
            {
                _projectile.projectileSprite = _projectile.endingSprite;

            }

        }

    }

}
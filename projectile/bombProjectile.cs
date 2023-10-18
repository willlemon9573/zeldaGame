using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{
    internal class bombProjectile : IProjectile
    {
        private double timer = 0; // Timer to track how long the bomb has been active
        private double waitingTime = 600; // The time in milliseconds before the bomb explodes
        ProjectileEntity _projectile; // Reference to the projectile entity representing the bomb

        public bombProjectile(ProjectileEntity ProjectileEntity)
        {
            _projectile = ProjectileEntity; // Initialize the bomb projectile with the provided entity
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= waitingTime)
            {
                // When the timer exceeds the waiting time, set the projectile sprite to the ending sprite
                _projectile.projectileSprite = _projectile.endingSprite;
            }
        }
    }
}

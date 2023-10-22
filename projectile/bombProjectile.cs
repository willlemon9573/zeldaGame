using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{
    internal class BombProjectile : IProjectile
    {
        private double timer = 0; // Timer to track how long the bomb has been active
        private double waitingTime = 600; // The time in milliseconds before the bomb explodes
        private IProjectileEntity _projectile; // Reference to the projectile entity representing the bomb

        /// <summary>
        /// Initializes a new instance of the BombProjectile class.
        /// </summary>
        /// <param name="ProjectileEntity">The projectile entity representing the bomb.</param>
        public BombProjectile(IProjectileEntity ProjectileEntity)
        {
            _projectile = ProjectileEntity; // Initialize the bomb projectile with the provided entity
        }

        /// <summary>
        /// Updates the bomb projectile's state over time.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= waitingTime)
            {
                // When the timer exceeds the waiting time, set the projectile sprite to the ending sprite
                _projectile.ProjectileSprite = _projectile.EndingSprite;
            }
        }
    }
}

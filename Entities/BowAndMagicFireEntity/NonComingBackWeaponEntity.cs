using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    /// <summary>
    /// Represents an abstract base class for non-returning projectile entities like arrows or magic fire.
    /// This class extends ProjectileEntity to provide shared behavior for non-returning weapons.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal abstract class NonComingBackWeaponEntity : ProjectileEntity
    {
        protected double timer; // Timer to track how long the projectile has been active
        protected const double WaitingTime = 150; // Time in milliseconds before the projectile ends
        protected int _maxDistance; // Maximum distance the projectile can travel
        protected float distanceMoved; // Distance the projectile has moved
        protected Vector2 _spriteMovingAddition; // Movement vector for the projectile
        protected bool IsActive; // Flag to indicate if the projectile is active

        /// <summary>
        /// Initializes a new instance of the NonComingBackWeaponEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public NonComingBackWeaponEntity(String weaponName) : base(weaponName)
        {
            _rotation = 0;
        }

        /// <summary>
        /// Draws the projectile on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public sealed override void Draw(SpriteBatch spriteBatch)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, _rotation);
        }

        /// <summary>
        /// Updates the state of the projectile entity.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public sealed override void Update(GameTime gameTime)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Update(gameTime);
            Animate(gameTime);
        }

        /// <summary>
        /// Animates the projectile, handling its movement and lifecycle.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for animation.</param>
        protected void Animate(GameTime gameTime)
        {
            if (!IsActive)
            {
                ProjectileSprite = ImpactEffectSprite;
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer >= WaitingTime)
                {
                    ProjectileSprite = null;
                    ImpactEffectSprite = null;
                    timer = 0;
                }
                return;
            }

            _weaponPosition += _spriteMovingAddition;
            distanceMoved += movingSpeed;

            if (distanceMoved >= _maxDistance)
            {
                IsActive = false; // Deactivate the projectile when it reaches max distance
            }
        }
    }
}
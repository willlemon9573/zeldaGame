using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

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
        protected bool _drawImpactSprite = false;
        protected SoundEffect _weaponSoundEffect;

        /// <summary>
        /// Initializes a new instance of the NonComingBackWeaponEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public NonComingBackWeaponEntity(string weaponName) : base(weaponName)
        {
            _rotation = 0;
            _weaponSoundEffect = SoundFactory.GetSound("arrow_boomerang");
            _weaponSprite = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(weaponName.ToLower());
        }

        /// <summary>
        /// Draws the projectile on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public sealed override void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive == false && _drawImpactSprite == true)
            {
                ImpactEffectSprite.Draw(spriteBatch, _weaponPosition, Color.White, _currentSpriteEffect, _rotation);
                return;
            }
            ProjectileSprite.Draw(spriteBatch, _weaponPosition, Color.White, _currentSpriteEffect, _rotation);
        }

        /// <summary>
        /// Updates the state of the projectile entity.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public sealed override void Update(GameTime gameTime)
        {
            if (_isActive == false) { return; }
            ProjectileSprite.Update(gameTime);
            Animate(gameTime);
            _projectileCollider.Update(this);
        }

        /// <summary>
        /// Animates the projectile, handling its movement and lifecycle.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for animation.</param>
        protected void Animate(GameTime gameTime)
        {
            _weaponPosition += _spriteMovingAddition;
            distanceMoved += movingSpeed;
            if (distanceMoved >= _maxDistance)
            {
                Stop();
            }
        }

        public virtual void Stop()
        {
            if (_isActive && GameStatesManager.CurrentState is GamePlayingState gamePlayingState)
            {
                _isActive = false;
                _drawImpactSprite = true;
                gamePlayingState.RemoveProjectile(this);
            }
        }
    }
}
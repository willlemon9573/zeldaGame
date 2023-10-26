using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.BoomerangEntity
{
    internal abstract class BoomerangBasedEntity : ProjectileEntity
    {
        protected int _maxDistance;
        protected bool returning = false;
        protected int distanceMoved = 0;
        protected Vector2 _spriteMovingAddition;
        protected bool IsActive = true; // Flag to indicate if the projectile is active
        protected readonly float RotationIncrement = MathHelper.ToRadians(10); // Increment for projectile rotation
        /// <summary>
        /// Entity for the Boomerang the player will use.
        /// @Author - ZiheWang
        /// </summary>
        public BoomerangBasedEntity(String weaponName) : base(weaponName)
        {
            _rotation = 0;
            //no constructor needed
        }

        public sealed override void Draw(SpriteBatch spriteBatch)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, _rotation);
        }

        public sealed override void Update(GameTime gameTime)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Update(gameTime);
            Animate();


        }
        protected void MoveProjectile(Vector2 _spriteMovingAddition)
        {
            _weaponPosition += _spriteMovingAddition;
        }
        protected void Animate()
        {
            _rotation += RotationIncrement;

            if (_rotation > MathHelper.TwoPi)
                _rotation -= MathHelper.TwoPi;

            if (!IsActive)
            {
                // If the projectile is not active, set its sprite and update function to null
                ProjectileSprite = null;
                return;
            }

            if (returning)
            {
                MoveProjectile(-_spriteMovingAddition); // Move the projectile back if it's returning
            }
            else
            {
                MoveProjectile(_spriteMovingAddition); // Move the projectile forward
            }
            distanceMoved += 1;

            if (distanceMoved >= _maxDistance && !returning)
            {
                returning = true; // Start returning when the maximum distance is reached
                distanceMoved = 0;
            }
            else if (distanceMoved >= _maxDistance && returning)
            {
                IsActive = false; // Deactivate the projectile when it returns and reaches max distance
            }
        }
    }
}
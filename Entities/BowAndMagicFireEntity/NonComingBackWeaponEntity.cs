using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    internal abstract class NonComingBackWeaponEntity : ProjectileEntity
    {
        /// <summary>
        /// Entity for the Bow the player will use.
        /// @Author - ZiheWang
        /// </summary>
        protected double timer; // Timer to track how long the projectile has been active
        protected const double WaitingTime = 150; // The time in milliseconds before the projectile ends
        protected int _maxDistance;
        protected float distanceMoved;
        protected Vector2 _spriteMovingAddition;
        protected bool IsActive; // Flag to indicate if the projectile is active

        public NonComingBackWeaponEntity(String weaponName) : base(weaponName)
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
            Animate(gameTime);


        }
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
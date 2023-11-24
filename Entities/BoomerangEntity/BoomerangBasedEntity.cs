using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;

namespace SprintZero1.Entities.BoomerangEntity
{
    /// <summary>
    /// Represents the base functionality for boomerang-based entities.
    /// This abstract class provides common behaviors and properties used by all boomerang entities.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal abstract class BoomerangBasedEntity : ProjectileEntity
    {
        // Variables to control the boomerang's behavior and state.
        protected int _maxDistance;
        protected bool returning = false;
        protected float distanceMoved = 0;
        protected Vector2 _spriteMovingAddition;
        protected bool IsActive = true;
        protected readonly float RotationIncrement = MathHelper.ToRadians(20);
        protected readonly IMovableEntity _player;
        protected float _speedFactor = 1.0f;
        protected bool _isAccelerating = false;

        /// <summary>
        /// Initializes a new instance of the BoomerangBasedEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        /// <param name="player">The player entity.</param>
        public BoomerangBasedEntity(String weaponName, IMovableEntity player) : base(weaponName)
        {
            _rotation = 0;
            _player = player;
        }

        /// <summary>
        /// Draws the boomerang on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public sealed override void Draw(SpriteBatch spriteBatch)
        {
            if (ProjectileSprite == null) return;
            ProjectileSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, _rotation);
        }

        /// <summary>
        /// Updates the state of the boomerang.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public sealed override void Update(GameTime gameTime)
        {
            if (!IsActive || ProjectileSprite == null) return;

            // Update the sprite animation.
            ProjectileSprite.Update(gameTime);
            _projectileCollider.Update(this);

            // Move the boomerang based on its current state.
            if (returning)
                ReturnBoomerang(gameTime);
            else
                ThrowBoomerang(gameTime);
        }

        // Helper method to update the boomerang's position.
        protected void MoveProjectile(Vector2 spriteMovingAddition)
        {
            _weaponPosition += spriteMovingAddition;
        }

        // Handles the behavior of the boomerang when thrown.
        protected void ThrowBoomerang(GameTime gameTime)
        {
            // Rotate the boomerang.
            _rotation += RotationIncrement;
            if (_rotation > MathHelper.TwoPi) _rotation -= MathHelper.TwoPi;

            // Decelerate the boomerang.
            if (_speedFactor > 0.1f && !_isAccelerating) _speedFactor -= 0.017f;

            // Calculate and apply the movement of the boomerang.
            Vector2 moveDirection = _spriteMovingAddition * _speedFactor;
            MoveProjectile(moveDirection);
            distanceMoved += movingSpeed * _speedFactor;

            // Check if the boomerang should start returning.
            if (distanceMoved >= _maxDistance)
            {
                returning = true;
                distanceMoved = 0;
                _isAccelerating = true;
                _speedFactor = 0.1f;
            }
        }

        public void ReturnBoomerang() {
            if (returning) { return; }
            returning = true;
            distanceMoved = 0;
            _isAccelerating = true;
            _speedFactor = 0.1f;
        }

        // Handles the behavior of the boomerang when returning.
        protected void ReturnBoomerang(GameTime gameTime)
        {
            // Rotate the boomerang.
            _rotation += RotationIncrement;
            if (_rotation > MathHelper.TwoPi) _rotation -= MathHelper.TwoPi;

            // Calculate the direction to the player.
            Vector2 moveDirection = _player.Position - _weaponPosition;
            if (moveDirection != Vector2.Zero) moveDirection.Normalize();

            // Accelerate the boomerang.
            if (_speedFactor < 1.0f && _isAccelerating) _speedFactor += 0.017f;
            else
            {
                _isAccelerating = false;
                _speedFactor = 1.0f;
            }

            // Move the boomerang towards the player.
            MoveProjectile(moveDirection * movingSpeed * _speedFactor);

            // Deactivate the boomerang when it reaches the player.
            if (Vector2.Distance(_weaponPosition, _player.Position) < 1.0f)
            {
                ProjectileSprite = null;
                IsActive = false;
                if (GameStatesManager.CurrentState is GamePlayingState gameState)
                {
                    gameState.RemoveProjectile(this);
                }
            }
        }
    }

}
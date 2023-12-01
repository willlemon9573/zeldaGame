using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    /// <summary>
    /// Represents an enhanced bow weapon entity in the game.
    /// This class extends NonComingBackWeaponEntity to provide specific behavior for the better bow.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BetterBowEntity : NonComingBackWeaponEntity
    {
        private const int BetterBowMaxDistance = 60; // Maximum distance the projectile can travel before becoming inactive
        private const float BetterBowMovingSpeed = 3;

        /// <summary>
        /// Initializes a new instance of the BetterBowEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public BetterBowEntity(String weaponName) : base(weaponName)
        {
            _maxDistance = BetterBowMaxDistance;
            movingSpeed = BetterBowMovingSpeed;
            this._weaponDamage = 4f; // default weapon damage for silver arrow is 4 hearts
        }

        /// <summary>
        /// Prepares the weapon for use by setting its sprite and initial state based on the direction.
        /// </summary>
        /// <param name="direction">The direction in which the weapon will be used.</param>
        /// <param name="position">The initial position of the weapon.</param>
        public override void UseWeapon(Direction direction, Vector2 position)
        {
            // Setting up the projectile and impact effect sprites
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateArrowSprite("better", direction);
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateEndSprite();

            // Adjusting position and sprite based on the direction
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            _projectileCollider = new PlayerProjectileCollider(_weaponPosition, new System.Drawing.Size(ProjectileSprite.Width, ProjectileSprite.Height));
            if (GameStatesManager.CurrentState is GamePlayingState gameState)
            {
                gameState.AddProjectile(this);
            }
            _weaponSoundEffect.Play();
        }
    }
}

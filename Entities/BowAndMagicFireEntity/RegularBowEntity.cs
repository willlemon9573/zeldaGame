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
    /// Represents a regular bow weapon entity in the game.
    /// This class extends NonComingBackWeaponEntity and provides specific behavior for a standard bow.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class RegularBowEntity : NonComingBackWeaponEntity
    {
        private const int RegularBowMaxDistance = 40; // Maximum distance the projectile can travel before becoming inactive
        private const float RegularBowMovingSpeed = 1.5f;

        /// <summary>
        /// Initializes a new instance of the RegularBowEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public RegularBowEntity(String weaponName) : base(weaponName)
        {
            _maxDistance = RegularBowMaxDistance;
            movingSpeed = RegularBowMovingSpeed;
            // Constructor initializes specific attributes for the regular bow
        }

        /// <summary>
        /// Prepares the weapon for use by setting its sprite and initial state based on the direction.
        /// </summary>
        /// <param name="direction">The direction in which the weapon will be used.</param>
        /// <param name="position">The initial position of the weapon.</param>
        public override void UseWeapon(Direction direction, Vector2 position)
        {
            if (IsActive) { return; }
            distanceMoved = 0;
            _isActive = true;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateArrowSprite("", direction);
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
        }
    }
}

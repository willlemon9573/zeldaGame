using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System;

namespace SprintZero1.Entities.WeaponEntities.BowAndMagicFireEntity
{
    /// <summary>
    /// Represents a magic fire weapon entity in the game.
    /// This class extends NonComingBackWeaponEntity and provides specific behavior for a magic fire weapon.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class MagicFireEntity : NonComingBackWeaponEntity
    {
        private const int RegularBowMaxDistance = 100; // Maximum distance the projectile can travel before becoming inactive
        private const float RegularBowMovingSpeed = 0.7f;

        /// <summary>
        /// Initializes a new instance of the MagicFireEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public MagicFireEntity(string weaponName) : base(weaponName)
        {
            _maxDistance = RegularBowMaxDistance;
            movingSpeed = RegularBowMovingSpeed;
            // Constructor initializes specific attributes for the magic fire weapon
        }

        /// <summary>
        /// Prepares the weapon for use by setting its sprite and initial state based on the direction.
        /// </summary>
        /// <param name="direction">The direction in which the weapon will be used.</param>
        /// <param name="position">The initial position of the weapon.</param>
        public override void UseWeapon(Direction direction, Vector2 position)
        {
            distanceMoved = 0;
            _isActive = true;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateMagicFireSprite();
            ImpactEffectSprite = null;

            // Adjusting position and sprite based on the direction
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteEffects.None;
            _weaponPosition = position + SpriteAdditions.Item2;
        }
    }
}

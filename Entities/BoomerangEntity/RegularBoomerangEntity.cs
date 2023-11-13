using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System;

namespace SprintZero1.Entities.BoomerangEntity
{
    /// <summary>
    /// Represents a regular boomerang weapon entity in the game.
    /// This class extends BoomerangBasedEntity and defines the behavior for a standard boomerang.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class RegularBoomerangEntity : BoomerangBasedEntity
    {
        // Constants defining the maximum distance the boomerang can travel and its moving speed
        private const int RegularBoomerangMaxDistance = 70;
        private const float RegularBoomerangMovingSpeed = 2.5f;

        /// <summary>
        /// Initializes a new instance of the RegularBoomerangEntity class.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        /// <param name="player">The player entity using the boomerang.</param>
        public RegularBoomerangEntity(String weaponName, IMovableEntity player) : base(weaponName, player)
        {
            _maxDistance = RegularBoomerangMaxDistance;
            movingSpeed = RegularBoomerangMovingSpeed;
        }

        /// <summary>
        /// Prepares the weapon for use by setting its initial state and sprite.
        /// </summary>
        /// <param name="direction">The direction in which the weapon will be used.</param>
        /// <param name="position">The initial position of the weapon.</param>
        public override void UseWeapon(Direction direction, Vector2 position)
        {
            _speedFactor = 1.0f;
            IsActive = true;
            returning = false;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateBoomerangSprite("");
            ImpactEffectSprite = null;

            // Adjusting position and sprite based on the direction.
            var spriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteEffects.None;
            _weaponPosition = position + spriteAdditions.Item2;
        }
    }
}

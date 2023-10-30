using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    internal class MagicFireEntity : NonComingBackWeaponEntity
    {
        /// <summary>
        /// Entity for the Bow the player will use.
        /// @Author - ZiheWang
        /// </summary>
		private const int RegularBowMaxDistance = 100; // Maximum distance the projectile can travel before becoming inactive
        private const float RegularBowMovingSpeed = 0.7f;

        public MagicFireEntity(String weaponName) : base(weaponName)
        {
            _maxDistance = RegularBowMaxDistance;
            movingSpeed = RegularBowMovingSpeed;
            //no constructor needed
        }

        public override void UseWeapon(Direction direction, Vector2 position)
        {
            distanceMoved = 0;
            IsActive = true;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateMagicFireSprite();
            ImpactEffectSprite = null;
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteEffects.None;
            _weaponPosition = position + SpriteAdditions.Item2;
        }

    }
}
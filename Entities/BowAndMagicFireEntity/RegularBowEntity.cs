using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    internal class RegularBowEntity : NonComingBackWeaponEntity
    {
        /// <summary>
        /// Entity for the Bow the player will use.
        /// @Author - ZiheWang
        /// </summary>
		private const int RegularBowMaxDistance = 40; // Maximum distance the projectile can travel before becoming inactive
        private const float RegularBowMovingSpeed = 1;
        public RegularBowEntity(String weaponName) : base(weaponName)
        {
            _maxDistance = RegularBowMaxDistance;
            movingSpeed = RegularBowMovingSpeed;
            //no constructor needed
        }

        public override void UseWeapon(Direction direction, Vector2 position)
        {
            distanceMoved = 0;
            IsActive = true;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateArrowSprite("", direction);
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateEndSprite();
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
        }

    }
}
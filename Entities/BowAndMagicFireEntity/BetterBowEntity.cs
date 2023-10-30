using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System;

namespace SprintZero1.Entities.BowAndMagicFireEntity
{
    internal class BetterBowEntity : NonComingBackWeaponEntity
    {
        private const int BetterBowMaxDistance = 60; // Maximum distance the projectile can travel before becoming inactive
        private const float BetterBowMovingSpeed = 3;
        /// <summary>
        /// Entity for the Bow the player will use.
        /// @Author - ZiheWang
        /// </summary>
        public BetterBowEntity(String weaponName) : base(weaponName)
        {
            _maxDistance = BetterBowMaxDistance;
            movingSpeed = BetterBowMovingSpeed;
            //no constructor needed
        }

        public override void UseWeapon(Direction direction, Vector2 position)
        {
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateArrowSprite("better", direction);
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateEndSprite();
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _spriteMovingAddition = _spriteMovingDictionary[direction] * movingSpeed;
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
        }

    }
}
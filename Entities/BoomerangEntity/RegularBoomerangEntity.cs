using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.BoomerangEntity
{
	internal class RegularBoomerangEntity : BoomerangBasedEntity
    {
        private const int RegularBoomerangMaxDistance = 50; // Maximum distance the projectile can travel before becoming inactive
        private const float RegularBoomerangMovingSpeed = 2.5f;
        /// <summary>
		/// Entity for the Bow the player will use.
        /// @Author - ZiheWang
        /// </summary>
        public RegularBoomerangEntity(String weaponName) : base(weaponName)
		{
            _maxDistance = RegularBoomerangMaxDistance;
            movingSpeed = RegularBoomerangMovingSpeed;
        }

		public override void UseWeapon(Direction direction, Vector2 position)
		{
			ProjectileSprite = WeaponSpriteFactory.Instance.CreateBoomerangSprite("");
			ImpactEffectSprite = null;
			Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
			_spriteMovingAddition = _spriteMovingDictionary[direction];
			_currentSpriteEffect = SpriteAdditions.Item1;
			_weaponPosition = position + SpriteAdditions.Item2;
		}

	}
}
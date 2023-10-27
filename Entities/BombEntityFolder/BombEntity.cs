using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.BombEntityFolder
{
    internal class BombEntity : ProjectileEntity
    {
        private double timer = 0; // Timer to track how long the bomb has been active
        private readonly double waitingTime = 600; // The time in milliseconds before the bomb explodes
        /// <summary>
        /// Entity for the Boomerang the player will use.
        /// @Author - ZiheWang
        /// </summary>
        public BombEntity(String weaponName) : base(weaponName)
        {
            _rotation = 0;
            //no constructor needed
        }

        public override void UseWeapon(Direction direction, Vector2 position)
        {
            timer = 0;
            ProjectileSprite = WeaponSpriteFactory.Instance.CreateBombSprite();
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateBombSpriteExplodes();
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteEffects.None;
            _weaponPosition = position + SpriteAdditions.Item2;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, _rotation);
        }

        public override void Update(GameTime gameTime)
        {
            if (ProjectileSprite == null)
            {
                return;
            }
            ProjectileSprite.Update(gameTime);
            Animate(gameTime);


        }
        private void Animate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= waitingTime)
            {
                // When the timer exceeds the waiting time, set the projectile sprite to the ending sprite
                ProjectileSprite = ImpactEffectSprite;
            }
        }
    }
}
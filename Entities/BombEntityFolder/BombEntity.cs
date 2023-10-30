using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.BombEntityFolder
{
    internal class BombEntity : IWeaponEntity
    {
        private double timer = 0; // Timer to track how long the bomb has been active
        private readonly double waitingTime = 600; // The time in milliseconds before the bomb explodes
        private readonly string _weaponName;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite;
        private ISprite ImpactEffectSprite;
        private const SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        private readonly Dictionary<Direction, Vector2> _spriteEffectsDictionary;
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }
        /// <summary>
        /// Entity for the Boomerang the player will use.
        /// @Author - ZiheWang
        /// </summary>
        public BombEntity(String weaponName)
        {
            _weaponName = weaponName;
            /* This might be able to be passed by the player / xml / or mathematically */
            _spriteEffectsDictionary = new Dictionary<Direction,Vector2>()
            {
                { Direction.North,  new Vector2(0, -11) },
                { Direction.South,  new Vector2(0, 11) },
                { Direction.East,   new Vector2(11, 0) },
                { Direction.West,   new Vector2(-11, 0) }
            };
            //no constructor needed
        }


        public void UseWeapon(Direction direction, Vector2 position)
        {
            timer = 0;
            _weaponSprite = WeaponSpriteFactory.Instance.CreateBombSprite();
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateBombSpriteExplodes();
            Vector2 SpriteAdditions = _spriteEffectsDictionary[direction];
            _weaponPosition = position + SpriteAdditions;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_weaponSprite == null)
            {
                return;
            }
            _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, 0);
        }

        public void Update(GameTime gameTime)
        {
            if (_weaponSprite == null)
            {
                return;
            }
            _weaponSprite.Update(gameTime);
            Animate(gameTime);


        }
        private void Animate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= waitingTime)
            {
                // When the timer exceeds the waiting time, set the projectile sprite to the ending sprite
                _weaponSprite = ImpactEffectSprite;
            }
        }
    }
}
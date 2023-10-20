using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Used for creating a melee weapon as melee weapons do not have a quanitity 
    /// @Author - Aaron Heishman
    /// </summary>
    internal class MeleeWeaponEntity : IEntity, IWeaponEntity
    {
        private readonly String _weaponName;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite = WeaponSpriteFactory.Instance.GetMeleeWeaponSprite("woodensword", Direction.North);
        private readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        /* total draw time and elapsed draw time will handle when a weapon should be drawn/undrawn and updated */
        private readonly float _totalDrawTime = 1 / 7f;
        private float _elapsedDrawTime;
        /* Sprite effect for flipping the weapon */
        private SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        Boolean _weaponUsed; /* Allow the weapon to be drawn/updated */
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }

        public MeleeWeaponEntity(String weaponName)
        {
            _weaponName = weaponName;
            _weaponUsed = false;
            /* This might be able to be passed by the player / xml / or mathematically */
            _spriteEffectsDictionary = new Dictionary<Direction, Tuple<SpriteEffects, Vector2>>()
            {
                { Direction.North, Tuple.Create(SpriteEffects.None, new Vector2(0, -11)) },
                { Direction.South, Tuple.Create(SpriteEffects.FlipVertically, new Vector2(0, 11)) },
                { Direction.East, Tuple.Create(SpriteEffects.None, new Vector2(11, 0)) },
                { Direction.West, Tuple.Create(SpriteEffects.FlipHorizontally, new Vector2(-11, 0)) }
            };
            _elapsedDrawTime = 0;
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            _weaponSprite = WeaponSpriteFactory.Instance.GetMeleeWeaponSprite(_weaponName, direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            _weaponUsed = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_weaponUsed)
            {
                _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, 0, 1f);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_weaponUsed)
            {
                _elapsedDrawTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_elapsedDrawTime <= _totalDrawTime)
                {
                    _weaponSprite.Update(gameTime);
                }
                else
                {
                    _weaponUsed = false;
                    _elapsedDrawTime = 0;
                }
            }
        }
    }
}

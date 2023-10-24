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
    /// Entity for the sword the player will use.
    /// @Author - Aaron Heishman
    /// </summary>
    internal class SwordEntity : IWeaponEntity
    {
        // TODO: Clean up code for modularity purposes
        private readonly string _weaponName;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite;
        /* Holds the specific values for properly flipping and placing sword in player's hands */
        private readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        /* Sprite effect for flipping the weapon */
        private SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }

        public SwordEntity(String weaponName)
        {
            _weaponName = weaponName;
            /* This might be able to be passed by the player / xml / or mathematically */
            _spriteEffectsDictionary = new Dictionary<Direction, Tuple<SpriteEffects, Vector2>>()
            {
                { Direction.North, Tuple.Create(SpriteEffects.None, new Vector2(0, -11)) },
                { Direction.South, Tuple.Create(SpriteEffects.FlipVertically, new Vector2(0, 11)) },
                { Direction.East, Tuple.Create(SpriteEffects.None, new Vector2(11, 0)) },
                { Direction.West, Tuple.Create(SpriteEffects.FlipHorizontally, new Vector2(-11, 0)) }
            };
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            _weaponSprite = WeaponSpriteFactory.Instance.GetSwordSprite(_weaponName, direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, 0, 1f);
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Add flashing effect if we want to have link shoot off projectile at full hearts

        }
    }
}

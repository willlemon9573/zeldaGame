using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Entity for the sword the player will use.
    /// @Author - Aaron Heishman
    /// </summary>
    internal class SwordEntity : IWeaponEntity, ICollidableEntity
    {
        // TODO: Clean up code for modularity purposes
        private readonly string _weaponName;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite;
        /* Holds the specific values for properly flipping and placing sword in player's hands */
        private readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        /* Holds the Collider rectangles for all 4 directions */
        private readonly Dictionary<Direction, Rectangle> _colliderRectanglesDictionary;
        /* Sprite effect for flipping the weapon */
        private SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }

        private ICollider _collider;
        /* Get collider */
        public ICollider Collider { get { return _collider; } }

        public SwordEntity(String weaponName)
        {
            _weaponName = weaponName;
            const int X = 11, Y = 11;
            /* This might be able to be passed by the player / xml / or mathematically */
            _spriteEffectsDictionary = new Dictionary<Direction, Tuple<SpriteEffects, Vector2>>()
            {
                { Direction.North, Tuple.Create(SpriteEffects.None, new Vector2(0, Y*-1)) },
                { Direction.South, Tuple.Create(SpriteEffects.FlipVertically, new Vector2(0, Y)) },
                { Direction.East, Tuple.Create(SpriteEffects.None, new Vector2(X, 0)) },
                { Direction.West, Tuple.Create(SpriteEffects.FlipHorizontally, new Vector2(X*-1, 0)) }
            };
            _colliderRectanglesDictionary = new Dictionary<Direction, Rectangle>()
            {
                {Direction.North, new Rectangle(5, Y*-1, 7, 16) },
                {Direction.South, new Rectangle(5, Y, 7, 16) },
                {Direction.East, new Rectangle(X, 5, 16, 7) },
                {Direction.West, new Rectangle(-X, 5, 16, 7) },
            };
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            _weaponSprite = WeaponSpriteFactory.Instance.GetSwordSprite(direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            Rectangle colliderRectangle = _colliderRectanglesDictionary[direction];
            colliderRectangle.Location += position.ToPoint();
            _collider = new DynamicCollider(colliderRectangle);  
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            EntityManager.AddImmediately(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, 0, 0.2f);

        }

        public void Update(GameTime gameTime)
        {
            // TODO: Add flashing effect if we want to have link shoot off projectile at full hearts
        }
    }
}

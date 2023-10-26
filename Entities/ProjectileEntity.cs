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
    /// Entity for the projectile the player will use.
    /// @Author - Aaron ZiheWang
    /// </summary>
    internal abstract class ProjectileEntity : IWeaponEntity
    {
        // TODO: Clean up code for modularity purposes
        protected readonly string _weaponName;
        protected Vector2 PlayerWeaponSprite;
        protected float _rotation;
        protected ISprite ProjectileSprite;
        protected ISprite ImpactEffectSprite;
        protected float movingSpeed;
        /* Holds the specific values for properly flipping and placing sword in player's hands */
        protected readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        protected readonly Dictionary<Direction, Vector2> _spriteMovingDictionary;
        /* Sprite effect for flipping the weapon */
        protected SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        protected Vector2 _weaponPosition;
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }

        protected ProjectileEntity(String weaponName)
        {
            _rotation = 0;
            _weaponName = weaponName;
            /* This might be able to be passed by the player / xml / or mathematically */
            _spriteEffectsDictionary = new Dictionary<Direction, Tuple<SpriteEffects, Vector2>>()
            {
                { Direction.North, Tuple.Create(SpriteEffects.None, new Vector2(0, -11)) },
                { Direction.South, Tuple.Create(SpriteEffects.FlipVertically, new Vector2(0, 11)) },
                { Direction.East, Tuple.Create(SpriteEffects.None, new Vector2(11, 0)) },
                { Direction.West, Tuple.Create(SpriteEffects.FlipHorizontally, new Vector2(-11, 0)) }
            };
            _spriteMovingDictionary = new Dictionary<Direction, Vector2>()
            {
                { Direction.North, new Vector2(0, -1) },
                { Direction.South, new Vector2(0, 1)},
                { Direction.East,  new Vector2(1, 0)},
                { Direction.West,  new Vector2(-1, 0) }
            };
        }
        public abstract void UseWeapon(Direction direction, Vector2 position);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}

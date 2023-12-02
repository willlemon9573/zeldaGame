using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Represents an abstract base class for projectile entities used by the player.
    /// This class defines common properties and methods for different types of projectiles.
    /// </summary>
    /// <author>Aaron, Zihe Wang</author>
    internal abstract class ProjectileEntity : IWeaponEntity, ICollidableEntity
    {
        // Variables to control projectile's behavior and appearance
        protected readonly string _weaponName; // Name of the weapon
        protected Vector2 PlayerWeaponSprite; // Sprite for the weapon used by the player
        protected float _rotation; // Rotation of the projectile
        protected ISprite ProjectileSprite; // Current sprite representing the projectile
        protected ISprite ImpactEffectSprite; // Sprite for the projectile's impact effect
        protected float movingSpeed; // Moving speed of the projectile
        protected bool _isActive; // Flag to indicate if the projectile is active
        protected float _weaponDamage = 0f;
        protected ISprite _weaponSprite;
        public bool IsActive { get { return _isActive; } set { _isActive = value; } }


        // Dictionaries to hold sprite effect and positioning based on direction
        protected readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        protected readonly Dictionary<Direction, Vector2> _spriteMovingDictionary;

        // Sprite effect for flipping the weapon
        protected SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        protected Vector2 _weaponPosition; // Position of the weapon

        // Collider for the projectile
        protected ICollider _projectileCollider;

        public Vector2 Position
        {
            get { return _weaponPosition; }
            set { _weaponPosition = value; }
        }

        public ICollider Collider { get { return _projectileCollider; } }

        public float WeaponDamage { get { return _weaponDamage; } }

        public ISprite Sprite { get { return _weaponSprite; } }

        protected ProjectileEntity(String weaponName)
        {
            _rotation = 0;
            _weaponName = weaponName;
            // Initialize dictionaries for sprite effects and movements
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
                { Direction.South, new Vector2(0, 1) },
                { Direction.East, new Vector2(1, 0) },
                { Direction.West, new Vector2(-1, 0) }
            };
        }

        // Abstract methods that must be implemented by derived classes
        public abstract void UseWeapon(Direction direction, Vector2 position);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}

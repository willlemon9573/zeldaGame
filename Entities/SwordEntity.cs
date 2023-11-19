using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.DebuggingTools;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Entity for the sword the player will use.
    /// @Author - Aaron Heishman
    /// </summary>
    internal class SwordEntity : IWeaponEntity, ICollidableEntity
    {
        const float Rotation = 0f;
        const float LayerDepth = 0.2f;
        const int ColliderX = 5;
        const int ColliderY = 11;
        const int ColliderWidth = 15;
        const int ColliderHeight = 20;
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
        SpriteDebuggingTools spriteDebugger;
        private ICollider _collider;
        /* Get collider */
        public ICollider Collider { get { return _collider; } }
        /// <summary>
        /// TODO: Remove weapon name if my inventory implementation works
        /// </summary>
        /// <param name="weaponName"></param>
        public SwordEntity(String weaponName, Dictionary<Direction, Tuple<SpriteEffects, Vector2>> spriteEffectsMap)
        {
            _weaponName = weaponName;
            _spriteEffectsDictionary = spriteEffectsMap;
            /* 
             * the values of this rectangle change based on the direction of the sword which is why each collider is also different 
             * I don't have time right now to add this to the parser to make just yet
            */
            _colliderRectanglesDictionary = new Dictionary<Direction, Rectangle>()
            {
                {Direction.North, new Rectangle(ColliderX, -ColliderY, ColliderWidth, ColliderHeight) },
                {Direction.South, new Rectangle(ColliderX, ColliderY, ColliderWidth, ColliderHeight) },
                {Direction.East, new Rectangle(ColliderY, ColliderX, ColliderHeight, ColliderWidth) },
                {Direction.West, new Rectangle(-ColliderY, ColliderX, ColliderHeight, ColliderWidth) },
            };
            spriteDebugger = new SpriteDebuggingTools(GameStatesManager.ThisGame);
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            _weaponSprite = WeaponSpriteFactory.Instance.GetSwordSprite(direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            _collider = new PlayerSwordCollider(_weaponPosition, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, Rotation, LayerDepth);
            spriteDebugger.DrawRectangle(_collider.Collider, Color.CornflowerBlue, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _collider.Update(this);
        }
    }
}

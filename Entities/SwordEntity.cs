using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        SoundEffect _swordSlash;
        private float _elapsedSoundTime;
        private const float TotalSoundTime = 0.26f;
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
            spriteDebugger = new SpriteDebuggingTools(GameStatesManager.ThisGame);
            _swordSlash = SoundFactory.GetSound("sword_slash");
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            _weaponSprite = WeaponSpriteFactory.Instance.GetSwordSprite(direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            _collider = new PlayerSwordCollider(_weaponPosition, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
            _swordSlash.Play();
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

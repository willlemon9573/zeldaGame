using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.DebuggingTools;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;
using System.Collections.Generic;

namespace SprintZero1.Entities.WeaponEntities
{
    /// <summary>
    /// Entity for the sword the player will use.
    /// @Author - Aaron Heishman
    /// </summary>
    internal class SwordEntity : IWeaponEntity, ICollidableEntity
    {
        private const float Rotation = 0f;
        private const float SwordDamage = 1f; // default sword damage is 1 heart
        private const float LayerDepth = 0.2f;
        private readonly string _weaponName;
        private readonly ISprite _defaultSprite;
        private float _stateElapsedTime = 0f;
        private readonly float _timeToResetState = 1 / 7f;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite;
        private Direction _direction;
        public Direction Direction { get { return _direction; } }
        /* Holds the specific values for properly flipping and placing sword in player's hands */
        private readonly Dictionary<Direction, Tuple<SpriteEffects, Vector2>> _spriteEffectsDictionary;
        /* Sprite effect for flipping the weapon */
        private SpriteEffects _currentSpriteEffect = SpriteEffects.None;
        public Vector2 Position { get { return _weaponPosition; } set { _weaponPosition = value; } }

        private SpriteDebuggingTools spriteDebugger;
        private ICollider _collider;
        private readonly SoundEffect _swordSlash;
        private bool _isActive;
        /* Get collider */
        public ICollider Collider { get { return _collider; } }

        public float WeaponDamage { get { return SwordDamage; } }

        public ISprite Sprite { get { return _defaultSprite; } }

        public bool IsActive { get { return _isActive; } }

        /// <summary>
        /// TODO: Remove weapon name if my inventory implementation works
        /// </summary>
        /// <param name="weaponName"></param>
        public SwordEntity(string weaponName, Dictionary<Direction, Tuple<SpriteEffects, Vector2>> spriteEffectsMap)
        {
            _weaponName = weaponName;
            _spriteEffectsDictionary = spriteEffectsMap;
            spriteDebugger = new SpriteDebuggingTools(GameStatesManager.ThisGame);
            _swordSlash = SoundFactory.GetSound("sword_slash");
            _defaultSprite = WeaponSpriteFactory.Instance.GetSwordSprite(Direction.North);
        }

        public void UseWeapon(Direction direction, Vector2 position)
        {
            if (_isActive) return;
            _direction = direction;
            _isActive = true;
            _weaponSprite = WeaponSpriteFactory.Instance.GetSwordSprite(direction);
            Tuple<SpriteEffects, Vector2> SpriteAdditions = _spriteEffectsDictionary[direction];
            _currentSpriteEffect = SpriteAdditions.Item1;
            _weaponPosition = position + SpriteAdditions.Item2;
            _collider = new PlayerSwordCollider(_weaponPosition, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
            _swordSlash.Play();
            if (GameStatesManager.CurrentState is GamePlayingState gameState)
            {
                gameState.AddCollider(this);
            }
            _stateElapsedTime = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _weaponSprite.Draw(spriteBatch, _weaponPosition, Color.White, _currentSpriteEffect, Rotation, LayerDepth);
            spriteDebugger.DrawRectangle(_collider.Collider, Color.CornflowerBlue, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            _collider.Update(this);
            _stateElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_stateElapsedTime >= _timeToResetState && GameStatesManager.CurrentState is GamePlayingState gameState)
            {
                _stateElapsedTime = 0f;
                gameState.RemoveCollider(this);
                _isActive = false;
            }
        }
    }
}

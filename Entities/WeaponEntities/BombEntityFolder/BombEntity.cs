using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;

namespace SprintZero1.Entities.WeaponEntities.BombEntityFolder
{
    /// <summary>
    /// Represents a bomb weapon entity in the game.
    /// This class handles the behavior and rendering of a bomb used by the player.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BombEntity : IWeaponEntity, ICollidableEntity
    {
        private double timer; // Timer to track how long the bomb has been active
        private readonly double waitingTime = 600; // Time in milliseconds before the bomb explodes
        private readonly float explosionTime = 1 / 2f;
        private float _explosionElapsedTime;
        private readonly string _weaponName;
        private const float BombDamage = 4f; // bomb deal 4 hearts
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite; // Current sprite representing the bomb
        private ISprite ImpactEffectSprite; // Sprite for the bomb's impact effect
        private const SpriteEffects _currentSpriteEffect = SpriteEffects.None; // Sprite effect for rendering
        private readonly Dictionary<Direction, Vector2> _spriteEffectsDictionary; // Effects based on direction
        private bool _isActive;
        private bool _hasExploded;
        private ICollider _bombCollider;
        private readonly SoundEffect _explosionSound;
        private readonly ISprite _onScreenSprite;

        public bool HasExploded { get { return _hasExploded; } }

        public Vector2 Position
        {
            get { return _weaponPosition; }
            set { _weaponPosition = value; }
        }

        public float WeaponDamage { get { return BombDamage; } }

        public ISprite Sprite { get { return _onScreenSprite; } }

        public ICollider Collider { get { return _bombCollider; } }

        public bool IsActive { get { return _isActive; } }

        /// <summary>
        /// Initializes a new instance of the BombEntity with a specific weapon name.
        /// </summary>
        /// <param name="weaponName">The name of the weapon.</param>
        public BombEntity(string weaponName)
        {
            _weaponName = weaponName;
            _spriteEffectsDictionary = new Dictionary<Direction, Vector2>()
            {
                { Direction.North, new Vector2(0, -11) },
                { Direction.South, new Vector2(0, 11) },
                { Direction.East, new Vector2(11, 0) },
                { Direction.West, new Vector2(-11, 0) }
            };
            _explosionSound = SoundFactory.GetSound("bomb_blow");
            _onScreenSprite = WeaponSpriteFactory.Instance.CreateBombSprite();
        }

        private void UpdateCollider()
        {
            if (GameStatesManager.CurrentState is GamePlayingState gameplayState)
            {
                gameplayState.AddProjectile(this);
            }
        }

        /// <summary>
        /// Activates the bomb weapon with a specified direction and position.
        /// </summary>
        /// <param name="direction">The direction of the bomb.</param>
        /// <param name="position">The position where the bomb is used.</param>
        public void UseWeapon(Direction direction, Vector2 position)
        {
            if (_isActive) { return; }
            timer = 0;
            _explosionElapsedTime = 0;
            _weaponSprite = WeaponSpriteFactory.Instance.CreateBombSprite();
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateBombSpriteExplodes();
            Vector2 SpriteAdditions = _spriteEffectsDictionary[direction];
            _weaponPosition = position + SpriteAdditions;
            _bombCollider = new PlayerBombCollider(position, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
            UpdateCollider();
            _hasExploded = false;
            _isActive = true;
        }

        /// <summary>
        /// Draws the bomb sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            _weaponSprite.Draw(spriteBatch, _weaponPosition, Color.White, _currentSpriteEffect, 0);
        }

        /// <summary>
        /// Updates the state of the bomb entity.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            _weaponSprite.Update(gameTime);
            Animate(gameTime);
            if (_hasExploded)
            {
                _bombCollider.Update(this);
                _explosionElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Handles the animation of the bomb, including transitioning to the impact effect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for animation.</param>
        private void Animate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= waitingTime && !_hasExploded)
            {
                _weaponSprite = ImpactEffectSprite; // Change to impact effect sprite after timer
                _bombCollider = new PlayerBombExplosionCollider(_weaponPosition, new System.Drawing.Size(_weaponSprite.Width, _weaponSprite.Height));
                _hasExploded = true;
                _explosionSound.Play();
            }
            else if (_hasExploded && _explosionElapsedTime >= explosionTime && GameStatesManager.CurrentState is GamePlayingState state)
            {
                state.RemoveProjectile(this);
                _isActive = false;
            }
        }
    }
}

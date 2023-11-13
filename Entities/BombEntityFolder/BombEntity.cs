using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Entities.BombEntityFolder
{
    /// <summary>
    /// Represents a bomb weapon entity in the game.
    /// This class handles the behavior and rendering of a bomb used by the player.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BombEntity : IWeaponEntity
    {
        private double timer; // Timer to track how long the bomb has been active
        private readonly double waitingTime = 600; // Time in milliseconds before the bomb explodes
        private readonly string _weaponName;
        private Vector2 _weaponPosition;
        private ISprite _weaponSprite; // Current sprite representing the bomb
        private ISprite ImpactEffectSprite; // Sprite for the bomb's impact effect
        private const SpriteEffects _currentSpriteEffect = SpriteEffects.None; // Sprite effect for rendering
        private readonly Dictionary<Direction, Vector2> _spriteEffectsDictionary; // Effects based on direction

        public Vector2 Position
        {
            get { return _weaponPosition; }
            set { _weaponPosition = value; }
        }

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
        }

        /// <summary>
        /// Activates the bomb weapon with a specified direction and position.
        /// </summary>
        /// <param name="direction">The direction of the bomb.</param>
        /// <param name="position">The position where the bomb is used.</param>
        public void UseWeapon(Direction direction, Vector2 position)
        {
            timer = 0;
            _weaponSprite = WeaponSpriteFactory.Instance.CreateBombSprite();
            ImpactEffectSprite = WeaponSpriteFactory.Instance.CreateBombSpriteExplodes();
            Vector2 SpriteAdditions = _spriteEffectsDictionary[direction];
            _weaponPosition = position + SpriteAdditions;
        }

        /// <summary>
        /// Draws the bomb sprite on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_weaponSprite == null)
            {
                return;
            }
            _weaponSprite.Draw(spriteBatch, _weaponPosition, _currentSpriteEffect, 0);
        }

        /// <summary>
        /// Updates the state of the bomb entity.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (_weaponSprite == null)
            {
                return;
            }
            _weaponSprite.Update(gameTime);
            Animate(gameTime);
        }

        /// <summary>
        /// Handles the animation of the bomb, including transitioning to the impact effect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for animation.</param>
        private void Animate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= waitingTime)
            {
                _weaponSprite = ImpactEffectSprite; // Change to impact effect sprite after timer
            }
        }
    }
}

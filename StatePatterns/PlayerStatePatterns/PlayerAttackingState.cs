using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// Handles the player when they're in the attacking state
    /// @author Aaron Heishman
    /// </summary>
    internal class PlayerAttackingState : BasePlayerState
    {
        private float _stateElapsedTime = 0f;
        private const float TimeToReset = 1 / 7f;
        IWeaponEntity _playerCurrentWeapon;

        /// <summary>
        /// Keep track of the time in the state and reset back to idle state when finished
        /// </summary>
        /// <param name="deltaTime">time since last update</param>
        private void TrackStateTime(float deltaTime)
        {
            _stateElapsedTime += deltaTime;
            if (_stateElapsedTime >= TimeToReset)
            {
                _playerEntity.PlayerSprite = _linkSpriteFactory.GetLinkSprite(_playerEntity.Direction);
                _blockTransition = false;
                _playerEntity.TransitionToState(State.Idle);
            }
        }
        /// <summary>
        /// Constructor for the state transition Player Attacking State
        /// </summary>
        /// <param name="playerEntity">The player entering the state</param>
        public PlayerAttackingState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        /// <summary>
        /// Request State to handle attacking if transition isn't blocked
        /// </summary>
        public override void Request()
        {
            if (_blockTransition) { return; }
            _blockTransition = true;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetAttackingSprite(_playerEntity.Direction);
            _stateElapsedTime = 0;
            _playerCurrentWeapon = _playerEntity.CurrentUsableWeapon;
            _playerCurrentWeapon.UseWeapon(_playerEntity.Direction, _playerEntity.Position);
        }
        /// <summary>
        /// Handles updating 
        /// </summary>
        /// <param name="gameTime">The current game time</param>
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerCurrentWeapon.Update(gameTime);
            TrackStateTime(deltaTime);
        }

        /// <summary>
        /// Changes direction of player
        /// </summary>
        /// <param name="newDirection">The new direction the player will face</param>
        public override void ChangeDirection(Direction newDirection)
        {
            // Uses parent implementation - can use if we want to spin link when attacking 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /* Check the direction of the player to see if we need to flip
             * the player sprite
             */
            SpriteEffects spriteEffects = _playerEntity.Direction == Direction.West
                ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // draw sprite
            _playerEntity.PlayerSprite.Draw(spriteBatch, _playerEntity.Position, spriteEffects, 0, 0.1f);
            _playerCurrentWeapon.Draw(spriteBatch);
        }
    }
}


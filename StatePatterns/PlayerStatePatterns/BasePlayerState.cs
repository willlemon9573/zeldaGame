﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// Abstract parent player state. handles default implementations of the player states
    /// @Author Aaron Heishman
    /// </summary>
    internal abstract class BasePlayerState : IPlayerState
    {
        protected PlayerEntity _playerEntity;
        protected PlayerSpriteFactory _playerSpriteFactory = PlayerSpriteFactory.Instance;
        protected bool _canTransition = true; // false by default
        protected string _characterName;

        /// <summary>
        /// Abstract base player constructor.
        /// Note: PlayerEntity already implements its contract - so passing the
        /// PlayerEntity to the constructor is allowed
        /// </summary>
        /// <param name="playerEntity">The specific PlayerEntity that uses the State Pattern</param>
        public BasePlayerState(PlayerEntity playerEntity)
        {
            this._playerEntity = playerEntity;
            _characterName = playerEntity.CharacterName;
        }
        /// <summary>
        /// Changes the direction of the player based on the current state
        /// </summary>
        /// <param name="newDirection">the new direction the player will face</param>
        public virtual void ChangeDirection(Direction newDirection)
        {
            if (!_canTransition) { return; }
            _playerEntity.Direction = newDirection;
            _playerEntity.PlayerSprite = _playerSpriteFactory.GetPlayerMovementSprite(_characterName, newDirection);
        }

        public virtual void TransitionState(IPlayerState newState)
        {
            if (!_canTransition) { return; }
            _playerEntity.PlayerState = newState;
        }

        /// <summary>
        /// Handles a request made by the player
        /// </summary>
        public abstract void Request();

        /// <summary>
        /// Update the player base on player State. Requires override
        /// </summary>
        /// <param name="gameTime">The current time state of the game</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Default implementation of Sprite drawing. overrideable in case extra logic is required
        /// </summary>
        /// <param name="spriteBatch">The sprite batch handler for drawing</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /* Check the direction of the player to see if we need to flip
             * the player sprite
             */
            SpriteEffects spriteEffects = _playerEntity.Direction == Direction.West
                ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // draw sprite
            _playerEntity.PlayerSprite.Draw(spriteBatch, _playerEntity.Position, Color.White, spriteEffects, 0, 0.1f);
        }

        /// <summary>
        /// Blocks player from transitioning to a new state. Overridable in case extra logic is required
        /// </summary>
        public virtual void BlockTransition()
        {
            _canTransition = false;
        }

        /// <summary>
        /// Unblocks player state transition. Overridable in case extra logic is required
        /// </summary>
        public virtual void UnblockTranstion()
        {
            _canTransition = true;
        }

        public virtual bool CanTransition()
        {
            return _canTransition;
        }
    }
}


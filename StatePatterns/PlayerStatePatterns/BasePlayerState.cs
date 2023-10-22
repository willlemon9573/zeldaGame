using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.Entities;
using System.Collections.Generic;
using System;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
	internal abstract class BasePlayerState : IPlayerState
	{
        protected PlayerEntity _playerEntity;
        protected State _playerPreviousState;
        private readonly Dictionary<State, Func<IPlayerState>> stateTransitionMap;

        /// <summary>
        /// Abstract base player constructor.
        /// Note: PlayerEntity already implements its contract - so passing the
        /// PlayerEntity to the constructor is allowed
        /// </summary>
        /// <param name="playerEntity">The specific PlayerEntity that uses the State Pattern</param>
		public BasePlayerState(PlayerEntity playerEntity)
		{
            this._playerEntity = playerEntity;
            stateTransitionMap = new Dictionary<State, Func<IPlayerState>>()
            {
                {State.Moving, () => new PlayerMovingState(_playerEntity) },
                {State.Attacking, () => new PlayerAttackingState(_playerEntity) },
                {State.Idle, () => new PlayerIdleState(_playerEntity) }
                // Add more states as needed
            };
		}

        public abstract void ChangeDirection(Direction newDirection);
        
        public virtual void TransitionState(State newState)
        {
            // return if player state hasn't changed else change player state
            if (_playerPreviousState == newState) { return; }
            _playerEntity.PlayerState = stateTransitionMap[newState].Invoke();
            _playerPreviousState = newState;
        }

        public abstract void Request();

        public abstract void Update(GameTime gameTime);
     
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /* Check the direction of the player to see if we need to flip
             * the player sprite
             */
            SpriteEffects spriteEffects = _playerEntity.Direction == Direction.West
                ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // draw sprite
            _playerEntity.PlayerSprite.Draw(spriteBatch, _playerEntity.Position, spriteEffects);
        }
    }
}


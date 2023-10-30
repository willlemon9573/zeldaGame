using Microsoft.Xna.Framework;
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
        private readonly float _timeToResetState = 1/7f;
        /// <summary>
        /// Keep track of the time in the state and reset back to idle state when finished
        /// </summary>
        /// <param name="deltaTime">time since last update</param>
        private void TrackStateTime(float deltaTime)
        {
            _stateElapsedTime += deltaTime;
            if (_stateElapsedTime >= _timeToResetState)
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
            /* Transition to state updates player state after invoking method. Track the previous state beforehand */
        }

        /// <summary>
        /// Request State to handle attacking if transition isn't blocked
        /// </summary>
        public override void Request()
        {
            if (_blockTransition) { return; }
            _blockTransition = true;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetAttackingSprite(_playerEntity.Direction);
        }
        /// <summary>
        /// Handles updating 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
    }
}


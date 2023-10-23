using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerAttackingState : BasePlayerState
    {
        private float _stateElapsedTime = 0f;
        private float _timeToResetState = 1 / 7f;

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

        public PlayerAttackingState(PlayerEntity playerEntity) : base(playerEntity)
        {
            /* Transition to state updates player state after invoking method. Track the previous state beforehand */
        }

        public override void ChangeDirection(Direction newDirection)
        {
            // Left unimplemented. Consider using if we implement a means for link to change direction while attacking (Charging bow for example)
        }

        public override void Request()
        {
            if (_blockTransition) { return; }
            _blockTransition = true;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetAttackingSprite(_playerEntity.Direction);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TrackStateTime(deltaTime);
        }
    }
}


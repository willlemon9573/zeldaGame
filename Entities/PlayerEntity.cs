﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;
using System.Diagnostics;

namespace SprintZero1.Entities
{
    internal class PlayerEntity : IEntity, IMovableEntity, ICombatEntity
    {
        /* Player Components */
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private PlayerCollider _playerCollider;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance;
        private float _timeElapsed;
        private float _timeToReset = 1f / 8;
        private State _oldState;

        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; } }

        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }

        public Direction Direction { get { return _playerDirection; } }

        private void Reset()
        {
            _timeElapsed = 0f;
            _playerSprite = _linkSpriteFactory.GetLinkSprite(_playerDirection);
            _playerStateMachine.ChangeState(State.Idle);
            _playerStateMachine.UnblockTransition();
        }

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public PlayerEntity(Vector2 position, int startingHealth, Direction startingDirection)
        {
            _playerDirection = startingDirection;
            _playerHealth = startingHealth;
            _playerPosition = position;
            _playerStateMachine = new PlayerStateMachine(State.Idle);
            // since we are currently only using link I'm setting this sprite here
            _playerSprite = _linkSpriteFactory.GetLinkSprite(startingDirection);
            _playerCollider = new PlayerCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16));
            _oldState = State.Idle;
        }

        public void Move(Vector2 distance)
        {
            if (_playerStateMachine.CanTransition())
            {
                _playerStateMachine.ChangeState(State.Moving);
                _playerPosition += distance;
            }
        }

        public void Attack()
        {
            bool canTransition = _playerStateMachine.CanTransition();
            State state = _playerStateMachine.GetCurrentState();
            if (state == State.Attacking || !canTransition) { return; }
            Debug.WriteLine("Player position: " + _playerPosition);
            // check if link can transition
            _playerStateMachine.BlockTransition();
            _playerStateMachine.ChangeState(State.Attacking);
            _playerSprite = _linkSpriteFactory.GetAttackingSprite(_playerDirection);
            _timeElapsed = 0f;
        }

        public void TakeDamage()
        {
            // not implemented
        }

        public void Die()
        {
            // not implemented
        }

        public void ChangeDirection(Direction direction)
        {
            if (_playerStateMachine.CanTransition())
            {
                _playerDirection = direction;
                _playerSprite = _linkSpriteFactory.GetLinkSprite(direction);
            }
        }

        public void Update(GameTime gameTime)
        {
            int keyCount = Keyboard.GetState().GetPressedKeyCount();
            bool canTransition = _playerStateMachine.CanTransition();
            State currentState = _playerStateMachine.GetCurrentState();
            _timeElapsed += (float)gameTime.TotalGameTime.TotalSeconds;
            if (keyCount == 0 && currentState != State.Idle && canTransition)
            {
                _playerStateMachine.ChangeState(State.Idle);
            }
            else if ((currentState == State.Moving || currentState == State.Attacking) && canTransition)
            {
                _playerSprite.Update(gameTime);
            }

            if (currentState == State.Attacking && _timeElapsed > (_timeToReset * 10))
            {
                Reset();
            }
            _playerCollider.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (_playerDirection == Direction.West)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            _playerSprite.Draw(spriteBatch, _playerPosition, spriteEffects);
        }
    }
}

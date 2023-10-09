﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;

namespace SprintZero1.Entities
{
    internal class PlayerEntity : IEntity, IMovableEntity, ICombatEntity
    {
        /* Player Components */
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance;

        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; } }

        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }

        public Direction Direction { get { return _playerDirection; } }

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
            // check if link can transition
            _playerStateMachine.BlockTransition();
            // set state to attacking
            // set time for link's attack animation
            // change link to his attack animation
            // checks
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
            _playerSprite.Update(gameTime);
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;
using System;
using System.Collections.Generic;
using SprintZero1.Enums;
using SprintZero1.Entities;
using SprintZero1.Controllers;
using SprintZero1.src;
using SprintZero1.Colliders;

namespace SprintZero1.Entities
{
    internal class PlayerEntity : IEntity
    {
        private Vector2 _position;
        public Vector2 Position { get { return _position; } set { _position = value; } }
        private ISprite _playerSprite;
        private PlayerStateMachine _playerStateMachine;
        private LinkSpriteFactory _playerFactory = LinkSpriteFactory.Instance;
        private IController _keyboardController;
        private IController _gamepadController;
        private ICollider _collider;
        private int _playerIndex;

        public PlayerEntity(ISprite sprite, Vector2 position, IController keyboardController)
        {
            _playerSprite = sprite;
            _position = position;
            _playerStateMachine = new PlayerStateMachine(Direction.South, MovementState.Idle);
            _keyboardController = keyboardController;
            _collider = new PlayerCollider(this, new Rectangle((int) position.X, (int) position.Y, 16, 16));
        }

        public PlayerEntity(ISprite sprite, Vector2 position, Game1 game, int PlayerIndex = 0)
        {
            _playerSprite = sprite;
            _position = position;
            _playerStateMachine = new PlayerStateMachine(Direction.South, MovementState.Idle);
            _keyboardController = new KeyboardController();
            _keyboardController.LoadDefaultCommands(game);
            _collider = new PlayerCollider(this, new Rectangle((int)position.X, (int)position.Y, 16, 16));
        }

        /// <summary>
        /// Update Link to a new position
        /// </summary>
        /// <param name="units">the amount (in pixels) that link's new position will be</param>
        public void Move(Vector2 units)
        {
            if (_playerSprite is AnimatedSprite)
            {
                _position += units;
            }
        }

        public void ChangeMovementState(MovementState newMovementState)
        {

            _playerStateMachine.ChangeMovementState(newMovementState);
        }

        public void ChangeDirection(Direction newDirection)
        {

            if (newDirection != _playerStateMachine.GetDirection() || _playerSprite is NonAnimatedSprite)
            {
                _playerStateMachine.ChangeDirection(newDirection);
            }
        }


        public MovementState GetMovementState()
        {
            return _playerStateMachine.GetMovementState();
        }

        public Direction GetDirection()
        {
            return _playerStateMachine.GetDirection();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects SE = SpriteEffects.None;
            if (_playerStateMachine.GetDirection() == Direction.West)
            {
                SE = SpriteEffects.FlipHorizontally;
            }
            ((AnimatedSprite) _playerSprite).Draw(spriteBatch, _position, SE, 1);
        }

        public void Update(GameTime gameTime)
        {
            _keyboardController.Update();
            _gamepadController.Update();
            _playerSprite.Update(gameTime);
            _collider.Update(gameTime);
        }
    }
}

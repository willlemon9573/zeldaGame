using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Aaron Heishman
    /// </summary>
    internal class PlayerEntity : IEntity, IMovableEntity, ICombatEntity
    {
        /* Player Components */
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private PlayerCollider _playerCollider;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance; // will be removed to give player a sprite on instantiation 
        /* controls the attacking state */
        private float _timeElapsed;
        private readonly float _timeToReset = 1f / 7;
        private IEntity _playerMainWeapon;
        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; } }

        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }

        public Direction Direction { get { return _playerDirection; } }

        private void Reset(float deltaTime)
        {
            _timeElapsed += deltaTime;
            if (_timeElapsed >= _timeToReset)
            {
                _playerSprite = _linkSpriteFactory.GetLinkSprite(_playerDirection);
                _playerStateMachine.ChangeState(State.Idle);
                _playerStateMachine.UnblockTransition();
                _timeElapsed = 0f;
            }
        }

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public PlayerEntity(Vector2 position, int startingHealth, Direction startingDirection)
        {
            /* Default values for player upon game start */
            _playerDirection = startingDirection;
            _playerHealth = startingHealth;
            _playerPosition = position;
            _playerStateMachine = new PlayerStateMachine(State.Idle);
            _playerSprite = _linkSpriteFactory.GetLinkSprite(startingDirection);
            _playerCollider = new PlayerCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16));
            _playerMainWeapon = new MeleeWeaponEntity("woodensword");
        }

        public void Move(Vector2 distance)
        {
            if (_playerStateMachine.CanTransition())
            {
                _playerStateMachine.ChangeState(State.Moving);
                _playerPosition += distance;
            }
        }

        public void Attack(string weaponName)
        {
            if (_playerStateMachine.CanTransition())
            {
                // check if link can transition
                _playerStateMachine.BlockTransition();
                _playerStateMachine.ChangeState(State.Attacking);
                _playerSprite = _linkSpriteFactory.GetAttackingSprite(_playerDirection);
                /* this will be changed to fit with projectiles */
                IWeaponEntity weaponRef = (IWeaponEntity)_playerMainWeapon;
                weaponRef.UseWeapon(Direction, _playerPosition);
            }
        }

        public void TakeDamage()
        {
            // not implemented yet
        }

        public void Die()
        {
            // not implemented yet
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
            /* Set player state to idle if no keys are pressed (will be changed for keyboard controller */
            if (keyCount == 0 && currentState != State.Idle && canTransition)
            {
                _playerStateMachine.ChangeState(State.Idle);
            }
            else if (currentState == State.Moving || currentState == State.Attacking)
            {
                /* Sprite only updates when player is moving / attacking */
                _playerMainWeapon.Update(gameTime);
                _playerSprite.Update(gameTime);
            }

            if (currentState == State.Attacking)
            {
                Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            _playerCollider.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (_playerDirection == Direction.West)
            {
                /* Considering adding this as an option for creating a sprite so it doesn't have to be called each time */
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            _playerMainWeapon.Draw(spriteBatch);
            _playerSprite.Draw(spriteBatch, _playerPosition, spriteEffects);
        }
    }
}

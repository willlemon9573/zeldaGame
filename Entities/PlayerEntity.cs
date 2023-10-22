using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.StatePatterns.PlayerStatePatterns;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Aaron Heishman
    /// </summary>
    internal class PlayerEntity : ICombatEntity
    {
        /* Player Components */
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private PlayerCollider _playerCollider;
        private readonly LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance; // will be removed to give player a sprite on instantiation 

        private float _timeElapsed;
        private readonly float _timeToReset = 1f / 7;
        private IEntity _playerMainWeapon;
        private IPlayerState _playerState;
        /* Public properties to modify the player's private members */
        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; } }
        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }
        public Direction Direction { get { return _playerDirection; } set { _playerDirection = value; } }
        public ISprite PlayerSprite { get { return _playerSprite; } set { _playerSprite = value; } }
        public IPlayerState PlayerState { get { return _playerState; } set { _playerState = value; } }

        
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
            _playerSprite = _linkSpriteFactory.GetLinkSprite(startingDirection);
            _playerCollider = new PlayerCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16), -3);
            _playerMainWeapon = new MeleeWeaponEntity("woodensword");

            _playerState = new PlayerIdleState(this);

        }

        public void Move()
        {
            if (_playerState is not PlayerMovingState) { TransitionToState(State.Moving); }
            _playerState.Request();
        }
        /// <summary>
        /// Transition player to the new state
        /// </summary>
        /// <param name="newState">the new state the player will transition to</param>
        public void TransitionToState(State newState)
        {
            _playerState.TransitionState(newState);
        }

        public void Attack(string weaponName)
        {
            // updating 
        }

        public void TakeDamage(int damage)
        {
            // not implemented yet
        }

        public void Die()
        {
            // not implemented yet
        }

        public void ChangeDirection(Direction direction)
        {
            _playerState.ChangeDirection(direction);
        }

        public void Update(GameTime gameTime)
        {

            _playerState.Update(gameTime);
            _playerCollider.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _playerState.Draw(spriteBatch);
        }
    }
}

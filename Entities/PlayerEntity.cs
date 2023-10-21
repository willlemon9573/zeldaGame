using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.CombatStatePatterns;
using SprintZero1.StatePatterns.MovingStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;

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
        private IEntity _playerMainWeapon;
        private IMovingEntityState _playerMovingState;
        private ICombatState _playerCombatState;
        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; } }

        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }

        public Direction Direction { get { return _playerDirection; } }

        public IMovingEntityState State { get { return _playerMovingState; } set { _playerMovingState = value; } }

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
            _playerMovingState = new IdleMovingState(this);
            _playerCombatState = new DisabledAttackingState(this);
        }

        public void Move(Vector2 distance)
        {
            _playerPosition += distance;
        }

        public void Attack(string weaponName)
        {
            // updating 
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
            _playerMovingState.ChangeDirection(direction);
            _playerSprite = _linkSpriteFactory.GetLinkSprite(direction);
            _playerDirection = direction;
        }

        public void Update(GameTime gameTime)
        {
            /* Set player state to idle if no keys are pressed (will be changed for keyboard controller */
            _playerMovingState.Update(gameTime);
            if (_playerMovingState is not IdleMovingState)
            {
                _playerSprite.Update(gameTime);
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

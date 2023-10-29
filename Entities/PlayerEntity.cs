using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.InventoryFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.PlayerStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Aaron Heishman
    /// </summary>
    internal class PlayerEntity : ICombatEntity, ICollidableEntity
    {
        /* Player Components */
        private int _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private PlayerCollider _playerCollider; // Not adding readonly modifier as colider may be an updatable in the future
        private readonly LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance; // will be removed to give player a sprite on instantiation 
        private IWeaponEntity _playerSwordSlot;
        private IWeaponEntity _playerEquipmentSlot;
        private IPlayerState _playerState;
        private bool _attackingWithSword = false;
        private PlayerInventory _playerInventory;
        /* Public properties to modify the player's private members */
        public int Health { get { return _playerHealth; } set { _playerHealth = value; } }
        public Direction Direction { get { return _playerDirection; } set { _playerDirection = value; } }
        public ISprite PlayerSprite { get { return _playerSprite; } set { _playerSprite = value; } }
        public IPlayerState PlayerState { get { return _playerState; } set { _playerState = value; } }
        public ICollider Collider { get { return _playerCollider; } }
        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; Collider.Update(this); } }
        public IWeaponEntity SwordSlot { get { return _playerSwordSlot; } set { _playerSwordSlot = value; } }
        public IWeaponEntity EquipmentSlot { get { return _playerEquipmentSlot; } set { _playerEquipmentSlot = value; } }

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
            _playerCollider = new PlayerCollider(new Rectangle((int)Position.X, (int)Position.Y, 16, 16), -3);
            _playerState = new PlayerIdleState(this);
            _playerInventory = new PlayerInventory(this);
            PlayerInventoryManager.AddPlayerInventory(this, _playerInventory);
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
            if (_playerState is not PlayerAttackingState) { TransitionToState(State.Attacking); }
            if (weaponName == "sword")
            {
                _attackingWithSword = true;
                _playerSwordSlot.UseWeapon(_playerDirection, _playerPosition);
            }
            PlayerState.Request();
        }

        public void TakeDamage(int damage)
        {
            // TODO: Implement in Sprint 4
        }

        public void Die()
        {
            // TODO: Implement in Sprint 4
        }

        public void ChangeDirection(Direction direction)
        {
            _playerState.ChangeDirection(direction);
        }

        public void Update(GameTime gameTime)
        {
            _playerState.Update(gameTime);
            _playerCollider.Update(this);
            if (_playerState is not PlayerIdleState && Keyboard.GetState().GetPressedKeyCount() == 0)
            {
                TransitionToState(State.Idle);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_playerState is PlayerAttackingState && _attackingWithSword)
            {
                _playerSwordSlot.Draw(spriteBatch);
            }
            else if (_playerState is not PlayerAttackingState && _attackingWithSword)
            {
                _attackingWithSword = false;
                EntityManager.RemoveImmediately(_playerSwordSlot);
            }
            _playerState.Draw(spriteBatch);

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.InventoryFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.GameStatePatterns;
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
        private float _playerMaxHealth; /* Link starts with 3 hearts */
        private float _playerHealth;
        private ISprite _playerSprite;
        private Direction _playerDirection;
        private Vector2 _playerPosition;
        private string _characterName;
        private ICollider _playerCollider; // Not adding readonly modifier as colider may be an updatable in the future
        private readonly PlayerSpriteFactory _linkSpriteFactory = PlayerSpriteFactory.Instance; // will be removed to give player a sprite on instantiation 
        private IWeaponEntity _playerSwordSlot;
        private IWeaponEntity _playerEquipmentSlot;
        private readonly PlayerStateFactory _playerStateFactory;
        private IPlayerState _playerState;
        private IPlayerState _playerVulnerabilityState;
        private IWeaponEntity _currentWeapon;
        private readonly PlayerInventory _playerInventory;
        private ILootableEntity _equipmentToDisplay;
        private bool _isDead;
        private bool _changeColliders;
        /* Public properties to modify the player's private members */
        public string name { get { return _characterName; } set { _characterName = value; } }
        public float Health { get { return _playerHealth; } set { _playerHealth = value; } }
        public float MaxHealth { get { return _playerMaxHealth; } set { _playerMaxHealth = value; } }
        public Direction Direction { get { return _playerDirection; } set { _playerDirection = value; } }
        public ISprite PlayerSprite { get { return _playerSprite; } set { _playerSprite = value; } }
        public IPlayerState PlayerState { get { return _playerState; } set { _playerState = value; } }
        public IPlayerState PlayerVulnerableState { get { return _playerVulnerabilityState; } set { _playerVulnerabilityState = value; } }
        public ICollider Collider { get { return _playerCollider; } set { _playerCollider = value; } }
        public Vector2 Position { get { return _playerPosition; } set { _playerPosition = value; Collider.Update(this); } }
        public IWeaponEntity SwordSlot { get { return _playerSwordSlot; } set { _playerSwordSlot = value; } }
        public IWeaponEntity EquipmentSlot { get { return _playerEquipmentSlot; } set { _playerEquipmentSlot = value; } }
        public IWeaponEntity CurrentUsableWeapon { get { return _currentWeapon; } set { _currentWeapon = value; } }
        public string CharacterName { get { return _characterName; } }
        public ILootableEntity EquipmentToDisplay { get { return _equipmentToDisplay; } set { _equipmentToDisplay = value; } }



        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="startingPosition">The starting position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public PlayerEntity(Vector2 startingPosition, string characterName, float startingHealth, Direction startingDirection)
        {
            /* Default values for player upon game start */
            _playerDirection = startingDirection;
            _playerHealth = startingHealth;
            _playerMaxHealth = startingHealth;
            _playerPosition = startingPosition;
            _characterName = characterName;
            _playerSprite = PlayerSpriteFactory.Instance.GetPlayerMovementSprite(characterName, startingDirection);
            float scalefactor = 0.9f; // scale factor for the collider
            linkHeight = _playerSprite.Height;
            linkWidth = _playerSprite.Width;
            _changeColliders = false;
            _playerCollider = new PlayerCollider(startingPosition, new System.Drawing.Size(_playerSprite.Width, _playerSprite.Height), scalefactor);
            _playerState = new PlayerIdleState(this);
            _playerVulnerabilityState = new PlayerVulnerableState(this);
            _playerInventory = new PlayerInventory(this);
            _isDead = false;
            _playerStateFactory = new PlayerStateFactory(this);
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
            _playerState.TransitionState(_playerStateFactory.GetPlayerState(newState));
        }

        public void Attack()
        {
            /* return if the player previously shot a projectile and it hasn't finished its animation */
            if (_currentWeapon.IsActive) { return; }
            if (_playerState is not PlayerAttackingState) { TransitionToState(State.Attacking); }
            _playerState.Request();
        }

        public void TakeDamage(float damage)
        {
            if (_playerVulnerabilityState is PlayerInvulnerabilityState) { return; }
            TransitionToState(State.TakingDamage);
            HUDManager.DecrementHealth(this, damage);
            _playerHealth -= damage;
            _playerState.Request();
            _playerVulnerabilityState = _playerStateFactory.GetPlayerState(State.Invulnerable);
            _playerVulnerabilityState.Request();
            if (_playerHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _isDead = true;
            if (GameStatesManager.CurrentState is GamePlayingState gameState)
            {
                gameState.DecrementLivePlayers();
            }
        }

        public void PickedupItem(ILootableEntity _equipment)
        {
            if (_playerState is not PlayerInteractingWithItemState) { TransitionToState(State.InteractingWithItem); } else { return; }
            EquipmentToDisplay = _equipment;
            _playerState.Request();
        }

        public void ChangeDirection(Direction direction)
        {
            _playerState.ChangeDirection(direction);
        }

        public void Update(GameTime gameTime)
        {
            if (_isDead)
            {
                if (_playerHealth > 1)
                {
                    _isDead = false;
                }
                return;
            }
            _playerState.Update(gameTime);
            if (_playerVulnerabilityState is PlayerInvulnerabilityState)
            {
                _playerVulnerabilityState.Update(gameTime); // update flashing if player is invulnerable
            }

            _playerCollider.Update(this);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isDead)
            {
                return;
            }

            if (_playerVulnerabilityState is PlayerInvulnerabilityState)
            {
                _playerVulnerabilityState.Draw(spriteBatch); // draw player as flashing when invulnerable
                if (_playerState is PlayerAttackingState)
                {
                    _playerState.Draw(spriteBatch); // draw weapon if player is also invulnerable
                }
            }
            else
            {
                _playerState.Draw(spriteBatch);
            }
        }
    }
}
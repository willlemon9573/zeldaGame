using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// State to handle when the player picks up a new item
    /// </summary>
    internal class PlayerInteractingWithItemState : BasePlayerState
    {
        private string SpriteName = "LinkInteracting";
        private const float TotalTime = 1 / 2f;
        private float _elapsedTime;
        private ILootableEntity _weaponToDisplay;
        private Vector2 offset;
        public PlayerInteractingWithItemState(PlayerEntity playerEntity) : base(playerEntity)
        {
            offset = new Vector2(0, -15);
            SpriteName = _characterName + "Interacting";
        }

        public override void Request()
        {
            if (_canTransition == false) { return; }
            BlockTransition();
            _playerEntity.PlayerSprite = _playerSpriteFactory.GetPlayerMovementSprite(SpriteName, Direction.South);
            _weaponToDisplay = _playerEntity.EquipmentToDisplay;
            _weaponToDisplay.Position = _playerEntity.Position;
            _weaponToDisplay.Position += offset; // place the item above the player
            _elapsedTime = 0f;
            if (GameStatesManager.CurrentState is GamePlayingState gameState)
            {
                gameState.AddProjectile(_weaponToDisplay);
                gameState.PauseUpdate = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsedTime >= TotalTime)
            {
                if (GameStatesManager.CurrentState is GamePlayingState gameState)
                {
                    gameState.RemoveProjectile(_weaponToDisplay);
                    gameState.PauseUpdate = false;
                }
                UnblockTranstion();
                _playerEntity.PlayerSprite = _playerSpriteFactory.GetPlayerMovementSprite(_characterName, Direction.South);
                _playerEntity.TransitionToState(State.Idle);
            }
        }
    }
}

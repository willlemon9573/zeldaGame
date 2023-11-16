using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    /// <summary>
    /// Handles picking up heart containers
    /// </summary>
    internal class PickUpHeartContainer : ICommand
    {
        private readonly ILootableEntity _heart;
        private readonly PlayerEntity _player;
        private readonly GamePlayingState _state;

        /// <summary>
        /// Constructor for picking up heart containers
        /// </summary>
        /// <param name="player">The player that picked up the heart</param>
        /// <param name="heartContainer">The heart container being picked up</param>
        public PickUpHeartContainer(ICollidableEntity player, ICollidableEntity heartContainer)
        {
            _player = player as PlayerEntity;
            _heart = heartContainer as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            _player.MaxHealth++;
            _player.Health = _player.MaxHealth;
            HUDManager.addNewHeart();
            _heart.Remove();
            _state.UpdateRoomEntities();
        }
    }
}

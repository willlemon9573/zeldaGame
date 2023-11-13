using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupReplenishingHeartCommand : ICommand
    {
        private readonly PlayerEntity _player;
        private readonly ILootableEntity _heart;
        private readonly GamePlayingState _state;

        public PickupReplenishingHeartCommand(ICollidableEntity player, ICollidableEntity heart)
        {
            _player = player as PlayerEntity;
            _heart = heart as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            _heart.Remove();
            _player.Health++;
            _state.UpdateRoomEntities();
        }
    }
}

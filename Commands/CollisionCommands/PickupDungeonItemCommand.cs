using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupDungeonItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;
        private readonly GamePlayingState _state;
        public PickupDungeonItemCommand(ICollidableEntity player, ICollidableEntity item)
        {
            _player = player;
            _item = item as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }
        public void Execute()
        {
            _item.Pickup(_player);
            _item.Remove();
            _state.UpdateRoomEntities();
        }
    }
}

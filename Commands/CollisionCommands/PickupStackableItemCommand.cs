using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickUpStackableItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;
        private readonly GamePlayingState _state;

        /// <summary>
        /// Constructor for picking up stackable items
        /// </summary>
        /// <param name="player">The player picking up the item</param>
        /// <param name="stackableItem">The item being picked up</param>
        public PickUpStackableItemCommand(ICollidableEntity player, ICollidableEntity stackableItem)
        {
            _player = player as IEntity;
            _item = stackableItem as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            _item.Pickup(_player, 1);
            _item.Remove();
            _state.UpdateRoomEntities();
        }
    }
}

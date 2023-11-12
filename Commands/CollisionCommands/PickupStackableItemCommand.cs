using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickUpStackableItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;

        /// <summary>
        /// Constructor for picking up stackable items
        /// </summary>
        /// <param name="player">The player picking up the item</param>
        /// <param name="stackableItem">The item being picked up</param>
        public PickUpStackableItemCommand(IEntity player, IEntity stackableItem)
        {
            _player = player;
            _item = stackableItem as ILootableEntity;
        }

        public void Execute()
        {
            _item.Pickup(_player, 1);
            _item.Remove();
            ProgramManager.UpdateRoomEntities();
        }
    }
}

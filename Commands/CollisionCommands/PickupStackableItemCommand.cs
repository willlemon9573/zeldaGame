using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickUpStackableItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;

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

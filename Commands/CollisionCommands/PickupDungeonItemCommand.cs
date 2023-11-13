using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupDungeonItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;
        public PickupDungeonItemCommand(ICollidableEntity player, ICollidableEntity item)
        {
            _player = player;
            _item = item as ILootableEntity;
        }
        public void Execute()
        {
            _item.Pickup(_player);
            _item.Remove();
        }
    }
}

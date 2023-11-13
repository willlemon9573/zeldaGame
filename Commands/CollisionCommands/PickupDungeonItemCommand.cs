using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupDungeonItemCommand : ICommand
    {
        public IEntity _player;
        public ILootableEntity _item;
        public PickupDungeonItemCommand(ICollidableEntity player, ICollidableEntity item)
        {
            _player = player;
            _item = item as ILootableEntity;
        }
        public void Execute()
        {
            _item.Pickup(_player);
            _item.Remove();
            ProgramManager.UpdateRoomEntities();
        }
    }
}

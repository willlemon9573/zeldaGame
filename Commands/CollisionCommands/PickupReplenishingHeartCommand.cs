using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupReplenishingHeartCommand : ICommand
    {
        private readonly PlayerEntity _player;
        private readonly ILootableEntity _heart;

        public PickupReplenishingHeartCommand(ICollidableEntity player, ICollidableEntity heart)
        {
            _player = player as PlayerEntity;
            _heart = heart as ILootableEntity;
        }

        public void Execute()
        {
            _heart.Remove();
            _player.Health++;
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupReplenishingHeartCommand : ICommand
    {
        private readonly PlayerEntity _player;
        private readonly ILootableEntity _heart;
        private float defaultAmount = 1f;

        public PickupReplenishingHeartCommand(ICollidableEntity player, ICollidableEntity heart)
        {
            _player = player as PlayerEntity;
            _heart = heart as ILootableEntity;
        }

        public void Execute()
        {
            _heart.Remove();
            float currentHealth = _player.Health;
            float maxhealth = _player.MaxHealth;
            if (currentHealth == maxhealth) { return; }

            float newHealth = currentHealth + defaultAmount;
            if (newHealth > maxhealth)
            {
                _player.Health = maxhealth;
            }
            else
            {
                _player.Health = newHealth;
            }

            HUDManager.IncrementHearts(defaultAmount);
        }
    }
}

using Microsoft.Xna.Framework.Audio;
using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Factories;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupReplenishingHeartCommand : ICommand
    {
        private readonly PlayerEntity _player;
        private readonly ILootableEntity _heart;
        private readonly float defaultAmount = 1f;
        private readonly SoundEffect _heartPickupSound;

        public PickupReplenishingHeartCommand(ICollidableEntity player, ICollidableEntity heart)
        {
            _player = player as PlayerEntity;
            _heart = heart as ILootableEntity;
            _heartPickupSound = SoundFactory.GetSound("get_heart");
        }

        public void Execute()
        {
            _heart.Remove();
            _heartPickupSound.Play();
            float currentHealth = _player.Health;
            float maxhealth = _player.MaxHealth;
            if (currentHealth == maxhealth) { return; } // return if player health is max
            float newHealth = currentHealth + defaultAmount;
            _player.Health = (newHealth >= maxhealth) ? newHealth : newHealth;
            HUDManager.IncrementHearts(_player, defaultAmount);
        }
    }
}

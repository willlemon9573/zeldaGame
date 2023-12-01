using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    /// <summary>
    /// Handles picking up heart containers
    /// </summary>
    internal class PickUpHeartContainer : ICommand
    {
        private readonly ILootableEntity _heart;
        private readonly PlayerEntity _player;

        /// <summary>
        /// Constructor for picking up heart containers
        /// </summary>
        /// <param name="player">The player that picked up the heart</param>
        /// <param name="heartContainer">The heart container being picked up</param>
        public PickUpHeartContainer(ICollidableEntity player, ICollidableEntity heartContainer)
        {
            _player = player as PlayerEntity;
            _heart = heartContainer as ILootableEntity;
        }

        public void Execute()
        {
            _player.MaxHealth++;
            _player.Health = _player.MaxHealth;
            HUDManager.IncreasePlayerHealth();
            _heart.Remove();
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithoutPlayer : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _equipment;
        private readonly GamePlayingState _state;
        public PickupEquipmentWithoutPlayer(ICollidableEntity player, ICollidableEntity equipmentItem)
        {
            _player = player;
            _equipment = equipmentItem as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {

            EquipmentItem equipment = (_equipment as EquipmentItemWithoutPlayerEntity).ItemType;
            /* Dropping two of each item now for multiplayer, return if one player already has an item so the other player can take it*/

            if (PlayerInventoryManager.PlayerContainsEquipment(_player, equipment)) { return; }
            IWeaponEntity weapon = WeaponEntityBuilder.CreateWeaponEntity(equipment);
            _equipment.Pickup(_player, weapon);
            _equipment.Remove();
        }
    }
}

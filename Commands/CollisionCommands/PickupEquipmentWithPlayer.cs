using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithPlayer : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _equipment;
        private readonly GamePlayingState _state;
        public PickupEquipmentWithPlayer(ICollidableEntity player, ICollidableEntity equipmentItem)
        {
            _player = player;
            _equipment = equipmentItem as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            EquipmentItem equipment = (_equipment as EquipmentItemWithPlayerEntity).ItemType;
            IWeaponEntity weapon = WeaponEntityBuilder.CreateWeaponEntity(equipment, _player as IMovableEntity);
            _equipment.Pickup(_player, weapon);
            _equipment.Remove();
            _state.UpdateRoomEntities();
        }
    }
}

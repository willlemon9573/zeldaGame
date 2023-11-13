using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithPlayer : ICommand
    {
        IEntity _player;
        ILootableEntity _equipment;
        public PickupEquipmentWithPlayer(ICollidableEntity player, ICollidableEntity equipmentItem)
        {
            _player = player;
            _equipment = equipmentItem as ILootableEntity;
        }

        public void Execute()
        {
            EquipmentItem equipment = (_equipment as EquipmentItemWithPlayerEntity).ItemType;
            IWeaponEntity weapon = WeaponEntityBuilder.CreateWeaponEntity(equipment, _player as IMovableEntity);
            _equipment.Pickup(_player, weapon);
            _equipment.Remove();
        }
    }
}

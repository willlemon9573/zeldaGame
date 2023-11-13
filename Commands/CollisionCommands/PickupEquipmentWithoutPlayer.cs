using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithoutPlayer : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _equipment;
        public PickupEquipmentWithoutPlayer(ICollidableEntity player, ICollidableEntity equipmentItem)
        {
            _player = player;
            _equipment = equipmentItem as ILootableEntity;
        }

        public void Execute()
        {
            EquipmentItem equipment = (_equipment as EquipmentItemWithoutPlayerEntity).ItemType;
            IWeaponEntity weapon = WeaponEntityBuilder.CreateWeaponEntity(equipment);
            _equipment.Pickup(_player, weapon);
            _equipment.Remove();
        }
    }
}

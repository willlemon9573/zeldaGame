using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithoutPlayer : ICommand
    {
        IEntity _player;
        ILootableEntity _equipment;
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
            ProgramManager.UpdateRoomEntities();
        }
    }
}

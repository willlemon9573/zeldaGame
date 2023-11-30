﻿using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupEquipmentWithPlayer : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _equipment;
        public PickupEquipmentWithPlayer(ICollidableEntity player, ICollidableEntity equipmentItem)
        {
            _player = player;
            _equipment = equipmentItem as ILootableEntity;
        }

        public void Execute()
        {
            EquipmentItem equipment = (_equipment as EquipmentItemWithPlayerEntity).ItemType;
            if (PlayerInventoryManager.PlayerContainsEquipment(_player, equipment)) { return; }
            IWeaponEntity weapon = WeaponEntityBuilder.CreateWeaponEntity(equipment, _player as IMovableEntity);
            _equipment.Pickup(_player, weapon);
            _equipment.Remove();
        }
    }
}

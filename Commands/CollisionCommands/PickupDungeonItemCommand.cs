using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupDungeonItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;

        private readonly Dictionary<DungeonItems, Action> handler = new Dictionary<DungeonItems, Action>()
            {
                { DungeonItems.Level1Map, () => HUDManager.AddMap() },
                { DungeonItems.Level1Compass, () => HUDManager.AddTriforceMarker() }
            };

        public PickupDungeonItemCommand(ICollidableEntity player, ICollidableEntity item)
        {
            _player = player;
            _item = item as ILootableEntity;

        }

        public void Execute()
        {
            _item.Remove();
            _item.Pickup(_player);
            handler[(_item as DungeonItemEntity).ItemType].Invoke();
            SoundFactory.PlaySound(SoundFactory.GetSound("get_item"));
        }
    }
}

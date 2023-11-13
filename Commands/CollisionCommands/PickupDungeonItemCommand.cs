using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupDungeonItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;
        private readonly GamePlayingState _state;
        private readonly Dictionary<DungeonItems, Action> handler;
        public PickupDungeonItemCommand(ICollidableEntity player, ICollidableEntity item)
        {
            _player = player;
            _item = item as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
            handler = new Dictionary<DungeonItems, Action>()
            {
                { DungeonItems.Level1Map, () => HUDManager.AddMap() },
                { DungeonItems.Level1Compass, () => HUDManager.AddTriforceMarker() }
            };
        }
        public void Execute()
        {
            _item.Pickup(_player);
            _item.Remove();
            DungeonItemEntity item = _item as DungeonItemEntity;
            handler[item.ItemType].Invoke();

            _state.UpdateRoomEntities();
        }
    }
}

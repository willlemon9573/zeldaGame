using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;
using System;
using SprintZero1.Factories;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickUpStackableItemCommand : ICommand
    {
        private readonly IEntity _player;
        private readonly ILootableEntity _item;
        private readonly GamePlayingState _state;
        private readonly Dictionary<StackableItems, Action> handler;

        /// <summary>
        /// Constructor for picking up stackable items
        /// </summary>
        /// <param name="player">The player picking up the item</param>
        /// <param name="stackableItem">The item being picked up</param>
        public PickUpStackableItemCommand(ICollidableEntity player, ICollidableEntity stackableItem)
        {
            _player = player as IEntity;
            _item = stackableItem as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
            handler = new Dictionary<StackableItems, Action>()
            {
                { StackableItems.Bomb, () => HUDManager.UpdateBombCount(1) },
                { StackableItems.DungeonKey, () => HUDManager.UpdateKeyCount(1) },
                { StackableItems.Rupee, () => HUDManager.UpdateRupeeCount(1) },
                { StackableItems.BlueRupee, () => HUDManager.UpdateRupeeCount(5)}
            };
        }

        public void Execute()
        {
            _item.Pickup(_player, 1);
            _item.Remove();
            StackableItemEntity item = _item as StackableItemEntity;
            handler[item.ItemType].Invoke();
            _state.UpdateRoomEntities();
            SoundFactory.PlaySound(SoundFactory.GetSound("get_item"));
        }
    }
}

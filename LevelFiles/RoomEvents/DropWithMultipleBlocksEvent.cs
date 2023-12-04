﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.LevelFiles.RoomEvents
{
    internal class DropWithMultipleBlocksEvent : IRoomEvent
    {
        private readonly DungeonRoom _room;
        List<Direction> _doorsToOpenDirections = new List<Direction>();
        private bool _canTriggerEvent;
        private readonly List<IMovableEntity> _movableBlocks;
        private readonly List<Vector2> _triggerPositions;
        private const string _direction = "East";
        private const int X = 167;
        private const int Y = 120;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="movableBlocks">List of movable blocks in level</param>
        /// <param name="triggerPositions">List of block destinations for event to trigger</param>
        /// <param name="requiredBlocks">Number of blocks needed to complete puzzle</param>
        public DropWithMultipleBlocksEvent(DungeonRoom room, List<IMovableEntity> movableBlocks, List<Vector2> triggerPositions, List<Direction> doorsToOpenDirections)
        {
            _room = room;
            _canTriggerEvent = true;
            _movableBlocks = movableBlocks;
            _triggerPositions = triggerPositions;
            _doorsToOpenDirections = doorsToOpenDirections;
        }

        /// <summary>
        /// Create the gun that will drop when the puzzle is complete
        /// </summary>
        /// <param name="offset">where the second bow will be placed offset from first for multiplaayer</param>
        /// <returns>the actual lootable entity</returns>
        private ILootableEntity CreateGun(int offset)
        {
            Direction gunDirection = (Direction)Enum.Parse(typeof(Direction), _direction, true); ;
            ISprite gunSprite = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite("betterbow");
            RemoveDelegate itemRemover = _room.RemoveAndSaveItem;
            EquipmentItemHandler itemHandler = PlayerInventoryManager.AddEquipmentItemToInventory;
            Vector2 dropPosition = new Vector2(X + offset, Y + offset);
            return new EquipmentItemWithoutPlayerEntity(gunSprite, dropPosition, itemRemover, itemHandler, EquipmentItem.BetterBow);
        }

        /// <summary>
        /// On/Off for if the event can be triggered or not, if already triggered then false
        /// </summary>
        /// <returns>Boolean</returns>
        public bool CanTriggerEvent()
        {
            return _canTriggerEvent;
        }

        /// <summary>
        /// Trigger the event if everything is satisfied, in this case the blocks are moved in place
        /// </summary>
        public void TriggerEvent()
        { 
            for (int i = _movableBlocks.Count - 1; i >= 0; i--)
            {
                //Debug.WriteLine($"Number {i}: {_movableBlocks[i].Position} = ${_triggerPositions[i]}");
                if (_movableBlocks[i].Position == _triggerPositions[i])
                {
                    _movableBlocks.Remove(_movableBlocks[i]);
                    _triggerPositions.Remove(_triggerPositions[i]);
                }
            }

            //all movableBlocks are in trigger positions, puzzle complete
            if (_movableBlocks.Count <= 0)
            {
                _room.AddRoomItem(CreateGun(offset: 0));
                _room.AddRoomItem(CreateGun(offset: 25));
                foreach (var direction in _doorsToOpenDirections)
                {
                    _room.UnlockDoor(direction);
                }
                SoundFactory.PlaySound(SoundFactory.GetSound("secret"));
                _canTriggerEvent = false;
            }
        }
    }
}
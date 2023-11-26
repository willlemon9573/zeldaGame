﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextRoomCommand : ICommand
    {
        private const int PositionOffsetOne = 115;
        private const int PositionOffsetTwo = 195;
        private Vector2 _playerSecretRoomPosition = new Vector2(56, 95); /* temporary fix until we can update the XML files to contain the player positions */
        private readonly ICollidableEntity _playerEntity;
        private readonly GamePlayingState _playingState;
        private readonly OpenDoorEntity _openDoor;
        private const string SecretRoom = "floorSecret";

        public Dictionary<Direction, Vector2> _directionMap;

        public EnterNextRoomCommand(ICollidableEntity playerEntity, ICollidableEntity doorEntity)
        {
            _playerEntity = playerEntity;
            _playingState = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
            _openDoor = doorEntity as OpenDoorEntity;
            _directionMap = new Dictionary<Direction, Vector2>() {
                { Direction.North, new Vector2(0, PositionOffsetOne) },
                { Direction.South, new Vector2(0, -PositionOffsetOne) },
                { Direction.East, new Vector2(-PositionOffsetTwo, 0) },
                { Direction.West, new Vector2(PositionOffsetTwo, 0) },
            };
        }

        public void Execute()
        {
            if (GameStatesManager.CurrentState is not GamePlayingState) { return; }
            string destination = _openDoor.DoorDestination;
            Vector2 playerCurrentPosition = _playerEntity.Position;
            if (destination != SecretRoom)
            {
                playerCurrentPosition += _directionMap[_openDoor.DoorDirection];
                HUDManager.UpdateMarker(_openDoor.DoorDirection);
            }
            else
            {
                playerCurrentPosition = _playerSecretRoomPosition;
            }
            _playerEntity.Position = playerCurrentPosition;
            _playingState.LoadDungeonRoom(destination);
            _playingState.Handle();
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System;
using System.Collections.Generic;
using SprintZero1.StatePatterns.PlayerStatePatterns;
namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextRoomCommand : ICommand
    {
        private const int MaxDirections = 4;
        private const int PositionOffsetOne = 110;
        private const int PositionOffsetTwo = 190;
        private Vector2 _playerSecretRoomPosition = new Vector2(56, 95); /* temporary fix until we can update the XML files to contain the player positions */
        private readonly ICollidableEntity _playerEntity;
        private readonly GamePlayingState _playingState;
        private readonly IDoorEntity _door;
        private const string SecretRoom = "floorSecret";
        private readonly ICommand pushBackCommand;

        public Dictionary<Direction, Vector2> _directionMap = new Dictionary<Direction, Vector2>() {
                { Direction.North, new Vector2(0, PositionOffsetOne) },
                { Direction.South, new Vector2(0, -PositionOffsetOne) },
                { Direction.East, new Vector2(-PositionOffsetTwo, 0) },
                { Direction.West, new Vector2(PositionOffsetTwo, 0) },
            };

        private bool PlayerCanTransition()
        {
            PlayerEntity player = _playerEntity as PlayerEntity;
            return player.PlayerState is not PlayerDamagedState;
        }

        public EnterNextRoomCommand(ICollidableEntity playerEntity, ICollidableEntity doorEntity)
        {
            _playerEntity = playerEntity;
            _playingState = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
            pushBackCommand = new PushBackCommand(playerEntity, doorEntity);
            _door = doorEntity as IDoorEntity;
        }

        public void Execute()
        {
            if (PlayerCanTransition() == false) {
                pushBackCommand.Execute();
                return; }
            string destination = _door.DoorDestination;
            Vector2 playerCurrentPosition = _playerEntity.Position;
            if (destination != SecretRoom)
            {
                playerCurrentPosition += _directionMap[_door.DoorDirection];
                HUDManager.UpdateMarker(_door.DoorDirection);
            }
            else
            {
                playerCurrentPosition = _playerSecretRoomPosition;
            }
            _playerEntity.Position = playerCurrentPosition;
            _playerEntity.Collider.Update(_playerEntity);
            _playingState.LoadDungeonRoom(destination);
            _playingState.Handle();

        }
    }
}

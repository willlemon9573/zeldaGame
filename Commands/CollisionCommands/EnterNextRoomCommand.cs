using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using SprintZero1.StatePatterns.PlayerStatePatterns;
using System.Collections.Generic;
namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextRoomCommand : ICommand
    {
        private const int PositionOffsetOne = 110;
        private const int PositionOffsetTwo = 190;
        private Vector2 _playerSecretRoomPosition = new Vector2(56, 95); /* temporary fix until we can update the XML files to contain the player positions */
        private readonly ICollidableEntity _playerEntity;
        private readonly GamePlayingState _playingState;
        private GameRoomTransitionState _transitionState;
        private readonly IDoorEntity _door;
        private const string SecretRoom = "floorSecret";
        private readonly ICommand pushBackCommand;

        public Dictionary<Direction, Vector2> _directionMap = new Dictionary<Direction, Vector2>() {
                { Direction.North, new Vector2(127, 204) },
                { Direction.South, new Vector2(127, 100) },
                { Direction.East, new Vector2(40, 152) },
                { Direction.West, new Vector2(215, 152) },
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
            if (PlayerCanTransition() == false)
            {
                pushBackCommand.Execute();
                return;
            }
            string destination = _door.DoorDestination;
            Vector2 playerNewPosition;
            if (destination != SecretRoom)
            {
                playerNewPosition = _directionMap[_door.DoorDirection];
                HUDManager.UpdateMarker(_door.DoorDirection);
            }
            else
            {
                playerNewPosition = _playerSecretRoomPosition;
            }

            GameStatesManager.ChangeGameState(GameState.RoomTransition);
            _transitionState = GameStatesManager.CurrentState as GameRoomTransitionState;

            _transitionState.StartTransition(destination, _door.DoorDirection, playerNewPosition);

            _playingState.LoadDungeonRoom(destination);
            _playingState.Handle();
        }
    }
}

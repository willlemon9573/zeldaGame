using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetPreviousLevelCommand : ICommand
    {
        private readonly List<string> _levelList;
        private readonly int _totalRooms;
        private int _index;
        private readonly GamePlayingState _gameState;

        public GetPreviousLevelCommand()
        {
            _levelList = LevelManager.DungeonRoomList;
            _totalRooms = _levelList.Count;
            _gameState = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            _index = LevelManager.CurrentRoomIndex;
            _index = ((_index - 1) + _totalRooms) % _totalRooms;
            LevelManager.CurrentRoomIndex = _index;
            string nextLevel = _levelList[_index];
            _gameState.LoadDungeonRoom(nextLevel, Direction.South);
        }
    }
}

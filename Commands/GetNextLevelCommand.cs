using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetNextLevelCommand : ICommand
    {
        private readonly List<string> levelList;
        private readonly int totalRooms;
        private int index;
        readonly GamePlayingState gameState;

        public GetNextLevelCommand()
        {
            levelList = LevelManager.DungeonRoomList;
            totalRooms = levelList.Count;
            gameState = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;

        }
        public void Execute()
        {
            index = LevelManager.CurrentRoomIndex;
            index = (index + 1) % totalRooms;
            LevelManager.CurrentRoomIndex = index;
            string nextLevel = levelList[index];
            gameState.LoadDungeonRoom(nextLevel);
        }
    }
}

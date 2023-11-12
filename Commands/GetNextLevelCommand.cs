using SprintZero1.Managers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Commands
{
    public class GetNextLevelCommand : ICommand
    {
        private readonly List<string> levelList;
        private readonly int totalRooms;
        private int index;

        public GetNextLevelCommand()
        {
            levelList = LevelManager.DungeonRoomList;
            totalRooms = levelList.Count;
        }
        public void Execute()
        {
            index = LevelManager.CurrentRoomIndex;
            index = (index + 1) % totalRooms;
            LevelManager.CurrentRoomIndex = index;
            string nextLevel = levelList[index];
            Debug.WriteLine(nextLevel);
            ProgramManager.ChangeRooms(nextLevel);
        }
    }
}

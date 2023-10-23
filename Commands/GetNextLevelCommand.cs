using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetNextLevelCommand : ICommand
    {
        private readonly List<String> levelList;
        private readonly int totalRooms;
        private int index;

        public GetNextLevelCommand()
        {
            levelList = LevelManager.LevelList;
            totalRooms = levelList.Count;
            index = LevelManager.LevelListIndex;
        }
        public void Execute()
        {
            index = (index + 1) % totalRooms;
            LevelManager.LevelListIndex = index;
            LevelManager.LoadNewRoom(levelList[index]);
        }
    }
}

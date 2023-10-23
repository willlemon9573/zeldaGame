using SprintZero1.Managers;
using SprintZero1.XMLFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    public class GetNextLevelCommand : ICommand
    {
        private readonly List<String> levelList;
        private readonly Game1 myGame;
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

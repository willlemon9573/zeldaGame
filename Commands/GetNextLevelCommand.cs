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
        private LevelManager levelManager;
        
        public GetNextLevelCommand(Game1 game)
        {
            myGame = game;
            levelList = game.LevelList;
            totalRooms = levelList.Count;
            index = game.LevelListIndex;
        }
        public void Execute()
        {
            index = (index + 1) % totalRooms;
            myGame.LevelListIndex = index;
            //levelManager.LoadRoom(levelList[index]);
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    public class GetPreviousLevelCommand : ICommand
    {
        private readonly List<String> levelList;
        private readonly Game1 myGame;
        private readonly int totalRooms;
        private int index;

        public GetPreviousLevelCommand()
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

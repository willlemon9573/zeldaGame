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
        private LevelManager levelManager;

        public GetPreviousLevelCommand(Game1 game)
        {
            myGame = game;
            levelManager = new LevelManager(game);
            levelList = game.LevelList;
            totalRooms = levelList.Count;
            index = game.LevelListIndex;
        }
        public void Execute()
        {
            index = ((index - 1) + totalRooms) % totalRooms;
            myGame.LevelListIndex = index;
             levelManager.LoadNewRoom(levelList[index]);
        }
    }
}

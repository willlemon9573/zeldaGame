using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    internal class EnterNextLevelCommand : ICommand
    {
        readonly int nextLevel;
        private readonly List<String> levelList;

        public EnterNextLevelCommand(PlayerEntity e1, LevelDoorEntity e2)
        {
            if (e2 != null)
            {
                nextLevel = e2._nextLevel;
                levelList = LevelManager.LevelList;
                Execute();
            }
        }

        public void Execute()
        {
            if (nextLevel > -1)
                LevelManager.LoadNewRoom(levelList[nextLevel]);
        }
    }
}

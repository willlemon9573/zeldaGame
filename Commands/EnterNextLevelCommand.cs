using SprintZero1.Colliders;
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

        public EnterNextLevelCommand(ICollider c1, LevelDoorCollider c2)
        {
            if (c1 != null && c2 != null)
            {
                nextLevel = ((LevelDoorEntity)c2.Parent)._nextLevel;
                levelList = LevelManager.LevelList;
                Execute();
            }
        }

        public void Execute()
        {
            if(nextLevel > -1)
                LevelManager.LoadNewRoom(levelList[nextLevel]);
        }
    }
}

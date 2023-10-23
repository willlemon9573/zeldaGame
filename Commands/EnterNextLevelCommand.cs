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

        public EnterNextLevelCommand(ICollider c1, ICollider c2)
        {
            if (c1 != null && c2 != null)
            {
                nextLevel = ((LevelDoorEntity)c2.Parent)._nextLevel;
                Execute();
            }
        }

        public void Execute()
        {
            LevelManager.LoadNewRoom(levelList[nextLevel]);
        }
    }
}

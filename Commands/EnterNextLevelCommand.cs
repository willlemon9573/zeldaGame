using SprintZero1.Entities;
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

                Execute();
            }
        }

        public void Execute()
        {

        }
    }
}

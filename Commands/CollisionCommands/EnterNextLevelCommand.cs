using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class EnterNextLevelCommand : ICommand
    {
        readonly int nextLevel;
        private readonly List<string> levelList;

        public EnterNextLevelCommand(PlayerEntity player, OpenDoorEntity e2)
        {

        }

        public void Execute()
        {

        }
    }
}

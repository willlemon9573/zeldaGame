using SprintZero1.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class ChangeLevelCommand : ICommand
    {
        public ChangeLevelCommand(ICollider c1, ICollider c2)
        {
            if (c1 != null && c2 != null)
                Execute();
        }

        public void Execute()
        {
            // ChangeLevel
        }
    }
}

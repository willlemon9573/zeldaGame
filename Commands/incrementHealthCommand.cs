using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class IncrementHealthCommand : ICommand
    {
        public IncrementHealthCommand() { }
        public void Execute()
        {
            float h = 1f;
            HUDManager.IncrementHearts(h, 4);

        }
    }
}

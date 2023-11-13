using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    public class DecrementHealthCommand : ICommand
    {
        public DecrementHealthCommand() { }
        public void Execute() {
            float h = 4f;
         HUDManager.DecrementHealth(h, 4);
            
        }
    }
}

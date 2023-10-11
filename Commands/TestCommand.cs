using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class TestCommand : ICommand
    {
        public void Execute()
        {
            Debug.WriteLine("Success!");
        }
    }
}

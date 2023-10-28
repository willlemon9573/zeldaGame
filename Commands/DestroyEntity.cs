using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands
{
    internal class DestroyEntity : ICommand
    {
        IEntity deadEntityWalking;

        public DestroyEntity(IEntity entityToLive, IEntity entityToDIE) 
        {
            deadEntityWalking = entityToDIE;
            Execute();
        }

        public void Execute()
        {
            EntityManager.Remove(deadEntityWalking);
        }
    }
}

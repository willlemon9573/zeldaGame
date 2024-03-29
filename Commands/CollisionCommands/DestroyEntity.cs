﻿using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class DestroyEntity : ICommand
    {
        private IEntity deadEntityWalking;

        public DestroyEntity(IEntity entityToLive, IEntity entityToDIE)
        {
            deadEntityWalking = entityToDIE;
        }

        public void Execute()
        {
            // Replace with kill entity code
        }
    }
}

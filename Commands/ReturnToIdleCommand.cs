﻿using SprintZero1.Entities;
using SprintZero1.StateMachines;

namespace SprintZero1.Commands
{

    internal class ReturnToIdleCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public ReturnToIdleCommand(IEntity entity)
        {
            _movableEntity = (IMovableEntity)entity;
        }

        public void Execute()
        {
            _movableEntity.State = new IdleEntityState(_movableEntity);
        }
    }
}

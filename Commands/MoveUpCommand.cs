﻿using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveUpCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveUpCommand(IMovableEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = entity;
        }

        public void Execute()
        {
            _movableEntity.ChangeDirection(Direction.North);
        }
    }
}

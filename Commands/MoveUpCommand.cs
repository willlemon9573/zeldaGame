using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveUpCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;

        public MoveUpCommand(IEntity entity)
        {
            _movableEntity = (IMovableEntity)entity;
        }

        public void Execute()
        {
            /* check the current state of the entity to see if they are able to move */
            if (_movableEntity.State == State.Attacking)
            {
                return;
            }
            else if (_movableEntity.State != State.Moving)
            {
                _movableEntity.State = State.Moving;
            }
            /* Check current direction */
            if (_movableEntity.Direction != Direction.North)
            {
                _movableEntity.ChangeDirection(Direction.North);
            }
            /* Update position of Entity */
            Vector2 newPosition = _movableEntity.Position;
            newPosition.Y -= 1;
            _movableEntity.Position = newPosition;
        }
    }
}

using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Numerics;

namespace SprintZero1.Commands
{
    internal class MoveRightCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;
        private readonly Vector2 _distance;

        public MoveRightCommand(IEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = (IMovableEntity)entity;
            _distance = new Vector2(1, 0);
        }

        public void Execute()
        {
            if (_movableEntity.Direction != Direction.West)
            {
                _movableEntity.ChangeDirection(Direction.East);
            }
            _movableEntity.Move(_distance);
        }
    }
}

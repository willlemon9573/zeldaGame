using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveLeftCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;
        private readonly Vector2 _distance;

        public MoveLeftCommand(IEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = (IMovableEntity)entity;
            _distance = new Vector2(-1, 0);
        }

        public void Execute()
        {
            if (_movableEntity.Direction != Direction.West)
            {
                _movableEntity.ChangeDirection(Direction.West);
            }
            _movableEntity.Move(_distance);
        }
    }
}

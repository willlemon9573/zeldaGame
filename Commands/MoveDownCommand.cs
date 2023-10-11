using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands
{
    internal class MoveDownCommand : ICommand
    {
        private readonly IMovableEntity _movableEntity;
        private readonly Vector2 _distance;

        public MoveDownCommand(IEntity entity)
        {
            /* Create a reference to the entity to access the move function */
            _movableEntity = (IMovableEntity)entity;
            _distance = new Vector2(0, 1);
        }

        public void Execute()
        {

            if (_movableEntity.Direction != Direction.South)
            {
                _movableEntity.ChangeDirection(Direction.South);
            }
            _movableEntity.Move(_distance);
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PushBlockCommand : ICommand
    {
        private readonly ICollidableEntity _playerEntity;
        private readonly ICollidableEntity _movableBlock;
        public PushBlockCommand(ICollidableEntity player, ICollidableEntity block)
        {
            _playerEntity = player;
            _movableBlock = block;
        }
        public void Execute()
        {
            Rectangle playerCollider = _playerEntity.Collider.Collider;
            Rectangle blockCollider = _movableBlock.Collider.Collider;
            Rectangle intersection = Rectangle.Intersect(playerCollider, blockCollider);

            /* Check if the collision is happening on the bottom. If it is then push the block up, else push player back */
            if (intersection.Height > 0 && playerCollider.Top == intersection.Top && blockCollider.Bottom == intersection.Bottom)
            {
                (_movableBlock as IMovableEntity).Move();
            }
            else
            {
                PriorityQueue<Vector2, float> pushBackDistance = new PriorityQueue<Vector2, float>();
                int commonZero = 0;
                int commonOne = 1;
                if (intersection.Width > intersection.Height) // push up
                {
                    pushBackDistance.Enqueue(new Vector2(commonZero, -commonOne), System.Math.Abs(intersection.Center.Y - blockCollider.Top));
                }
                else // push left right
                {
                    pushBackDistance.Enqueue(new Vector2(commonOne, commonZero), System.Math.Abs(intersection.Center.X - blockCollider.Right));
                    pushBackDistance.Enqueue(new Vector2(-commonOne, commonZero), System.Math.Abs(intersection.Center.X - blockCollider.Left));
                }

                _playerEntity.Position += pushBackDistance.Dequeue();
                _playerEntity.Collider.Update(_playerEntity);
            }
        }
    }
}

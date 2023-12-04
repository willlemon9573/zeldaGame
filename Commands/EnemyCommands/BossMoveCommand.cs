using Microsoft.Xna.Framework;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;

namespace SprintZero1.Commands.EnemyCommands
{
    internal class BossMoveCommand : ICommand
    {
        private readonly ICombatEntity _boss;
        private Rectangle _bossMovementBoundary;
        private const int MaxDirections = 4;
        /// <summary>
        /// Gets the opposite direction for the boss. Left as a simple command to move the object in one of two directions
        /// </summary>
        /// <returns>The new direction of the boss</returns>
        private Direction GetDirection()
        {
            int directionIndex = ((int)_boss.Direction + 2) % MaxDirections;
            Direction d = (Direction)directionIndex;
            return d;
        }

        /// <summary>
        /// Checks if the boss has tried to leave the boundary.
        /// </summary>
        /// <returns>True if the boss has left its boundary and needs to change direction, false otherwise</returns>
        private bool ChangeDirection()
        {
            if (_boss is ICollidableEntity collidableBoss)
            {
                Rectangle bossCollider = collidableBoss.Collider.Collider;
                Point _bossPositionPoint = new(bossCollider.X, bossCollider.Y);
                if (_bossMovementBoundary.Contains(_bossPositionPoint))
                {
                    return false;
                }
            }

            return true;
        }

        public BossMoveCommand(ICombatEntity boss, Rectangle boundary)
        {
            _boss = boss;
            _bossMovementBoundary = boundary;
        }
        public void Execute()
        {
            if (ChangeDirection())
            {
                _boss.Direction = GetDirection();
            }
            _boss.Move();
        }
    }
}

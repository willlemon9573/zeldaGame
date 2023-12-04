using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    /// <summary>
    /// Handles picking up heart containers
    /// </summary>
    internal class PauseEnemyCommand : ICommand
    {
        private readonly EnemyBasedEntity _enemy;
        private readonly BoomerangBasedEntity _boomerang;

        private const int MaxDirections = 4;
        private void ChangeEntityDirection()
        {
            if (_boomerang is BoomerangBasedEntity boomerang && _enemy is ICombatEntity enemy && enemy is not AquamentusEntity)
            {
                int dirIndex = ((int)boomerang.Direction + 2) % MaxDirections;
                Direction newDirection = (Direction)dirIndex;
                enemy.ChangeDirection(newDirection);
            }
        }
        public PauseEnemyCommand(ICollidableEntity boomerang, ICollidableEntity enemy)
        {
            _enemy = enemy as EnemyBasedEntity;
            _boomerang = boomerang as BoomerangBasedEntity;
        }

        public void Execute()
        {
            if (_boomerang.CollidedWithObject) { return; }
            _boomerang.CollidedWithObject = true;
            _boomerang.ForceReturnBoomerang();
            _enemy.TakeDamage(_boomerang.WeaponDamage);

        }
    }
}
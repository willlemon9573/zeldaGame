using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class SwordEnemyCollisionCommand : ICommand
    {
        private readonly float _weaponDamage;
        private readonly ICombatEntity _enemy;
        private ICollidableEntity _sword;
        private const int MaxDirections = 4;
        private void ChangeEntityDirection()
        {
            if (_sword is SwordEntity sword && _enemy is ICombatEntity enemy && enemy is not AquamentusEntity)
            {
                int dirIndex = ((int)sword.Direction + 2) % MaxDirections;
                Direction newDirection = (Direction)dirIndex;
                enemy.ChangeDirection(newDirection);
            }
        }
        public SwordEnemyCollisionCommand(ICollidableEntity sword, ICollidableEntity enemy)
        {
            _sword = sword;
            _weaponDamage = (sword as IWeaponEntity).WeaponDamage;
            _enemy = enemy as ICombatEntity;
        }
        public void Execute()
        {
            ChangeEntityDirection();
            _enemy.TakeDamage(_weaponDamage);
        }
    }
}

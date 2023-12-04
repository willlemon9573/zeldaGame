using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;
using System.Collections.Generic;

namespace SprintZero1.Commands.CollisionCommands
{
    /// <summary>
    /// Handles picking up heart containers
    /// </summary>
    internal class PauseEnemyCommand : ICommand
    {
        private readonly List<string> weak_enemies = new List<string>() // list of weak enemies that can take damage to the boomerang
        {
            { "dungeon_keese" },
            { "dungeon_gel" }
        };
        private readonly EnemyBasedEntity _enemy;
        private readonly BoomerangBasedEntity _boomerang;
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
            if (weak_enemies.Contains(_enemy.EnemyName.ToLower()))
            {
                _enemy.TakeDamage(_boomerang.WeaponDamage);

            }
            else
            {
                _enemy.PauseEnemy();
            }

        }
    }
}
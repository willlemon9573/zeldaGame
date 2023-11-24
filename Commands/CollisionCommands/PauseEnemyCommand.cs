using SprintZero1.Entities;
using SprintZero1.Entities.BoomerangEntity;


namespace SprintZero1.Commands.CollisionCommands
{
    /// <summary>
    /// Handles picking up heart containers
    /// </summary>
    internal class PauseEnemyCommand : ICommand
    {
        private readonly EnemyBasedEntity _enemy;
        private readonly BoomerangBasedEntity _boomerang;
        public PauseEnemyCommand(ICollidableEntity boomerang, ICollidableEntity enemy) {
            _enemy = enemy as EnemyBasedEntity;
            _boomerang = boomerang as BoomerangBasedEntity;
        }

        public void Execute()
        {
            _enemy.PauseEnemy();
            _boomerang.ReturnBoomerang();
        }
    }
}
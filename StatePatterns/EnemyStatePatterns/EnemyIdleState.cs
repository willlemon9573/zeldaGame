using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    internal class EnemyIdleState : BaseEnemyState
    {
        public EnemyIdleState(EnemyBasedEntity enemyEntity) : base(enemyEntity) { }

        public override void ChangeDirection(Direction newDirection)
        {
            // Idle enemy does not change direction
        }

        public override void Request()
        {
            // Idle enemy does not handle a request
        }

        public override void Update(GameTime gameTime)
        {
            // Idle enemy isn't updated
        }
    }
}


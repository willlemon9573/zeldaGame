using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    /// <summary>
    /// To be implemented in Sprint 4
    /// </summary>
	internal class EnemyKnockBackState : BaseEnemyState
    {
        public EnemyKnockBackState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
            //TODO: Implement logic
        }

        public override void ChangeDirection(Direction newDirection)
        {
            //TODO: Implement logic if needed
        }

        public override void Request()
        {
            //TODO: Implement the request handling logic
        }

        // Add override for Draw if needed

        public override void Update(GameTime gameTime)
        {
            //TODO: Implement Update logic
        }
    }
}


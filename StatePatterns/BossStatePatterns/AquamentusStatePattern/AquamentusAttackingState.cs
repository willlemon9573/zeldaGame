using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.EnemyStatePatterns;
using System;

namespace SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern
{
    internal class AquamentusAttackingState : BaseEnemyState
    {
        public AquamentusAttackingState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
        }

        public override void ChangeDirection(Direction newDirection)
        {
            throw new NotImplementedException();
        }

        public override void Request()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

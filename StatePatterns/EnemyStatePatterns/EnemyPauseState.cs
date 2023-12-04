using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    /// <summary>
    /// Placeholder class until we implement interacting / pausing
    /// </summary>
    internal class EnemyPauseState : BaseEnemyState
    {
        private const float MaxPauseTime = 2f;
        private float _elapsedPauseTime;
        public EnemyPauseState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
            _elapsedPauseTime = 0f;
        }

        public override void ChangeDirection(Direction newDirection)
        {
            //Player doesn't change direction when paused
        }

        public override void Request()
        {
            if (_blockTransition) { return; }
            _elapsedPauseTime = 0f;
            BlockTransition();
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedPauseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedPauseTime >= MaxPauseTime)
            {
                UnblockTranstion();
                TransitionState(State.Moving);
            }
        }
    }
}


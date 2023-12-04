using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    /// <summary>
    /// Handles the player when they are in the moving state
    /// @author ZiheWang
    /// </summary>
    internal class EnemyMovingState : BaseEnemyState
    {
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        public EnemyMovingState(EnemyBasedEntity enemyEntity) : base(enemyEntity)
        {
            _velocityMap = new Dictionary<Direction, Vector2>()
            {
                {Direction.North, new Vector2(0, -0.7f) },
                {Direction.South, new Vector2(0, 0.7f) },
                {Direction.East, new Vector2(0.7f, 0) },
                {Direction.West, new Vector2(-0.7f, 0) }
            };
        }

        public override void ChangeDirection(Direction newDirection)
        {
            if (_enemyEntity.Direction != newDirection)
            {
                string enemyName = _enemyEntity.EnemyName;
                _enemyEntity.Direction = newDirection;
                _enemyEntity.EnemySprite = _enemySpriteFactory.CreateEnemySprite(enemyName, newDirection);
            }
        }

        public override void Request()
        {
            if (_blockTransition) { return; }
            _enemyEntity.Position += _velocityMap[_enemyEntity.Direction];
        }

        public override void Update(GameTime gameTime)
        {
            _enemyEntity.EnemySprite.Update(gameTime);
        }

    }
}


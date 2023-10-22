using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;

namespace SprintZero1.Controllers.EnemyControllers
{
    internal class RandomEnemyMovementController : IEnemyMovementController
    {
        private readonly ICombatEntity _enemyEntity;
        private readonly Random _random;
        private readonly float _moveSpeed = 30f; // Set this value to whatever speed you want for the enemy
        private double _timeSinceLastDirectionChange;
        private readonly double _directionChangeInterval = 0.2; // The enemy changes direction every 2 seconds

        public RandomEnemyMovementController(ICombatEntity enemyEntity)
        {
            _enemyEntity = enemyEntity;
            _random = new Random();
        }

        public void Update(GameTime gameTime)
        {
            // Calculate elapsed time since last update
            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastDirectionChange += elapsed;

            // Change direction at the set interval or if the enemy is not moving (i.e., direction is None)
            if (_timeSinceLastDirectionChange >= _directionChangeInterval)
            {
                // Randomly pick a new direction
                Array values = Enum.GetValues(typeof(Direction));
                Direction randomDirection = (Direction)values.GetValue(_random.Next(values.Length));

                // Update the enemy's direction using the method from the entity
                _enemyEntity.ChangeDirection(randomDirection);
                _timeSinceLastDirectionChange = 0;
            }

            _enemyEntity.Move();

        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;

namespace SprintZero1.Controllers.EnemyControllers
{
    internal class RandomEnemyMovementController
    {
        private readonly ICombatEntity _enemyEntity;
        private readonly Random _random;
        private readonly float _moveSpeed = 100f; // Set this value to whatever speed you want for the enemy
        private double _timeSinceLastDirectionChange;
        private readonly double _directionChangeInterval = 2.0; // The enemy changes direction every 2 seconds

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
            if (_timeSinceLastDirectionChange >= _directionChangeInterval || _enemyEntity.Direction == Direction.None)
            {
                // Randomly pick a new direction
                Array values = Enum.GetValues(typeof(Direction));
                Direction randomDirection = (Direction)values.GetValue(_random.Next(values.Length));

                // Update the enemy's direction using the method from the entity
                _enemyEntity.ChangeDirection(randomDirection);
                _timeSinceLastDirectionChange = 0;
            }

            // Compute the new position based on the current direction
            Vector2 distanceToMove;

            switch (_enemyEntity.Direction)
            {
                case Direction.North:
                    distanceToMove = new Vector2(0, -_moveSpeed * (float)elapsed); // move up
                    break;
                case Direction.South:
                    distanceToMove = new Vector2(0, _moveSpeed * (float)elapsed); // move down
                    break;
                case Direction.East:
                    distanceToMove = new Vector2(_moveSpeed * (float)elapsed, 0); // move right
                    break;
                case Direction.West:
                    distanceToMove = new Vector2(-_moveSpeed * (float)elapsed, 0); // move left
                    break;
                default:
                    distanceToMove = Vector2.Zero; // no movement
                    break;
            }

            // Move the enemy by the distance vector
            _enemyEntity.Move(distanceToMove);

        }
    }
}

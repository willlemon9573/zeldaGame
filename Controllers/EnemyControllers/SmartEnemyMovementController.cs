using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using System;
using System.Collections.Generic;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// The SmartEnemyMovementController class handles intelligent movement for an enemy entity
    /// in relation to the player's position, using A* pathfinding.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class SmartEnemyMovementController : IEnemyMovementController
    {
        private readonly ICombatEntity _enemyEntity;
        private readonly IEntity _playerEntity;
        private readonly AStarPathfinder pathfinder;
        private Stack<Vector2> currentPath;
        private bool isPathBeingCalculated;
        private readonly double _pathCalculationInterval = 2.0;
        private double _timeSinceLastPathCalculation;
        private readonly double _moveTime = 4.0;
        private double _currentMoveTime = 0;
        private readonly double _stopTime = 1.0; // Unit is seconds
        private double _currentStopTime = 0;
        private bool _isMoving = true;
        private const int BlockSize = 16;
        private readonly double _directionChangeCooldown = 0.5;
        private double _timeSinceLastDirectionChange = 0;
        private readonly RemoveDelegate _remove;
        private bool _running;

        public SmartEnemyMovementController(ICombatEntity enemyEntity, IEntity playerEntity, RemoveDelegate remover)
        {
            _enemyEntity = enemyEntity;
            _playerEntity = playerEntity;
            pathfinder = new AStarPathfinder();
            currentPath = new Stack<Vector2>();
            isPathBeingCalculated = false;
            _remove = remover;
            _running = true;
        }

        public void Start()
        {
            _running = true;
        }

        public void Stop()
        {
            _running = false;
        }

        private Direction CalculateDirection(Vector2 moveDirection)
        {
            // Determines the direction based on the vector to the next step
            if (Math.Abs(moveDirection.X) > Math.Abs(moveDirection.Y))
            {
                return moveDirection.X > 0 ? Direction.East : Direction.West;
            }
            else
            {
                return moveDirection.Y > 0 ? Direction.South : Direction.North;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_running == false) { return; }
            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastPathCalculation += elapsed;
            _currentMoveTime += elapsed;
            _timeSinceLastDirectionChange += elapsed;  // Track time since last direction change

            // Trigger pathfinding at regular intervals
            if (_timeSinceLastPathCalculation >= _pathCalculationInterval && !isPathBeingCalculated)
            {
                pathfinder.StartFindingPath(_enemyEntity.Position, _playerEntity.Position);
                isPathBeingCalculated = true;
                _timeSinceLastPathCalculation = 0;
            }

            // Update path once calculated
            if (isPathBeingCalculated && pathfinder.Update())
            {
                currentPath = pathfinder.GetPath();
                isPathBeingCalculated = false;
            }

            // Handle movement and stopping intervals
            if (_isMoving)
            {
                if (_currentMoveTime >= _moveTime)
                {
                    _isMoving = false;
                    _currentMoveTime = 0;
                    _currentStopTime = elapsed;
                }
                else if (currentPath != null && currentPath.Count > 0)
                {
                    Vector2 nextStep = currentPath.Peek();
                    Vector2 moveDirection = nextStep - _enemyEntity.Position;
                    moveDirection.Normalize();
                    Direction newDirection = CalculateDirection(moveDirection);

                    // Change direction after cooldown
                    if (_timeSinceLastDirectionChange >= _directionChangeCooldown)
                    {
                        _enemyEntity.ChangeDirection(newDirection);
                        _timeSinceLastDirectionChange = 0;
                    }

                    _enemyEntity.Move();

                    // Pop the next step off the path once reached
                    if (Vector2.Distance(_enemyEntity.Position, nextStep) < 1.0f)
                    {
                        currentPath.Pop();
                    }
                }
            }
            else
            {
                _currentStopTime += elapsed;
                if (_currentStopTime >= _stopTime)
                {
                    _isMoving = true;
                    _currentStopTime = 0;
                }
            }

            if (_enemyEntity.Health <= 0)
            {
                _enemyEntity.Die();
                _remove(_enemyEntity);
                Stop();
            }
        }
    }
}

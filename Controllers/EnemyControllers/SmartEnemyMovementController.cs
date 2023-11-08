using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;
using System.Collections.Generic;

namespace SprintZero1.Controllers.EnemyControllers
{
    internal class SmartEnemyMovementController : IEnemyMovementController
    {
        private readonly ICombatEntity _enemyEntity;
        private double _timeSinceLastPathCalculation;
        private readonly double _pathCalculationInterval = 2.0;
        private readonly IEntity _playerEntity;
        private Stack<Vector2> currentPath;
        private readonly AStarPathfinder pathfinder;
        private bool isPathBeingCalculated;
        private readonly double _moveTime = 4.0;
        private readonly double _stopTime = 1.0; //unite is s
        private double _currentMoveTime = 0;
        private double _currentStopTime = 0;
        private bool _isMoving = true;
        private const int BlockSize = 16;
        private double _directionChangeCooldown = 0.5; 
        private double _timeSinceLastDirectionChange = 0; 


        public SmartEnemyMovementController(ICombatEntity enemyEntity, IEntity playerEntity)
        {
            _enemyEntity = enemyEntity;
            _playerEntity = playerEntity;
            pathfinder = new AStarPathfinder();
            currentPath = new Stack<Vector2>();
            isPathBeingCalculated = false;
        }

        private Direction CalculateDirection(Vector2 moveDirection)
        {
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
            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastPathCalculation += elapsed;
            _currentMoveTime += elapsed;
            _timeSinceLastDirectionChange += elapsed;  // Track the time since last direction change

            if (_timeSinceLastPathCalculation >= _pathCalculationInterval && !isPathBeingCalculated)
            {
                pathfinder.StartFindingPath(_enemyEntity.Position, _playerEntity.Position);
                isPathBeingCalculated = true;
                _timeSinceLastPathCalculation = 0;
            }

            if (isPathBeingCalculated && pathfinder.Update())
            {
                currentPath = pathfinder.GetPath();
                isPathBeingCalculated = false;
            }

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

                    // Check if enough time has passed since the last direction change
                    if (_timeSinceLastDirectionChange >= _directionChangeCooldown)
                    {
                        _enemyEntity.ChangeDirection(newDirection);
                        _timeSinceLastDirectionChange = 0;  // Reset the direction change timer
                    }

                    _enemyEntity.Move();

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
        }



    }
}
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System;

namespace SprintZero1.Controllers.EnemyControllers
{
    internal class SmartEnemyMovementController : IEnemyMovementController
    {
        private readonly ICombatEntity _enemyEntity;
        private readonly float _moveSpeed = 30f; 
        private double _timeSinceLastPathCalculation;
        private readonly double _pathCalculationInterval = 2.0; 
        private readonly IEntity _playerEntity;
        private Stack<Vector2> currentPath;
        private AStarPathfinder pathfinder;
        private bool isPathBeingCalculated;

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

            if (_timeSinceLastPathCalculation >= _pathCalculationInterval && !isPathBeingCalculated)
            {
                pathfinder.StartFindingPath(_enemyEntity.Position, _playerEntity.Position);
                isPathBeingCalculated = true;
                _timeSinceLastPathCalculation = 0;
            }

            
            if (isPathBeingCalculated)
            {
                Vector2 moveDirection = _playerEntity.Position - _enemyEntity.Position;
                moveDirection.Normalize();
                Direction direction = CalculateDirection(moveDirection);
                _enemyEntity.ChangeDirection(direction);
                _enemyEntity.Move(); 
            }

            
            if (isPathBeingCalculated && pathfinder.Update())
            {
                currentPath = pathfinder.GetPath();
                isPathBeingCalculated = false;
            }

           
            if (currentPath != null && currentPath.Count > 0)
            {
                Vector2 nextStep = currentPath.Peek();
                Vector2 moveDirection = nextStep - _enemyEntity.Position;

                moveDirection.Normalize();

                Direction direction = CalculateDirection(moveDirection);

                _enemyEntity.ChangeDirection(direction);

                _enemyEntity.Move(); 

                if (Vector2.Distance(_enemyEntity.Position, nextStep) < 1.0f)
                {
                    currentPath.Pop();
                }
            }
        }
    }
}

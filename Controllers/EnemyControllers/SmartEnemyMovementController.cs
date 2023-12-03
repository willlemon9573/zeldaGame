using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        private RemoveDelegate _remove;
        private List<IEntity> _architechtureList;
        private readonly Random _random = new Random();
        private readonly List<IEntity> _players;
        private bool _running;


        public SmartEnemyMovementController(ICombatEntity enemyEntity, List<IEntity> players, RemoveDelegate remover, List<IEntity> ArchitechtureList)
        {
            _architechtureList = ArchitechtureList;
            _enemyEntity = enemyEntity;
            _players = players;
            pathfinder = new AStarPathfinder(_architechtureList);
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
        private bool ShouldUseBoomerangAttack(Vector2 enemyPosition, Vector2 playerPosition)
        {
            float optimalBoomerangDistance = 100.0f;

            float distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition);
            return distanceToPlayer <= optimalBoomerangDistance;
        }


        public void Update(GameTime gameTime)
        {
            if (_running == false) { return; }
            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastPathCalculation += elapsed;
            _currentMoveTime += elapsed;
            _timeSinceLastDirectionChange += elapsed;  // Track time since last direction change
            IEntity nearestPlayer = FindNearestPlayer(_enemyEntity.Position, _players);
            if (nearestPlayer != null)
            {
                // Trigger pathfinding at regular intervals
                if (_timeSinceLastPathCalculation >= _pathCalculationInterval && !isPathBeingCalculated)
                {
                    pathfinder.StartFindingPath(_enemyEntity.Position, nearestPlayer.Position);
                    isPathBeingCalculated = true;
                    _timeSinceLastPathCalculation = 0;
                }

                if (_enemyEntity is EnemyBasedEntity enemyBasedEntity)
                {
                    if (ShouldUseBoomerangAttack(_enemyEntity.Position, nearestPlayer.Position))
                    {
                        Debug.Print("enemyAttacked");
                        enemyBasedEntity.Attack();
                    }
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
                        Vector2 nextStep;
                        if (_random.NextDouble() > 0.3)
                        {
                            nextStep = currentPath.Peek();
                        }
                        else
                        {
                            nextStep = GenerateRandomDirection(_enemyEntity.Position);
                        }
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

        private IEntity FindNearestPlayer(Vector2 enemyPosition, List<IEntity> players)
        {
            return players.OrderBy(p => Vector2.Distance(enemyPosition, p.Position)).FirstOrDefault();
        }

        private Vector2 GenerateRandomDirection(Vector2 currentPosition)
        {
            int stepSize = 5;

            var directionOffsets = new Dictionary<Direction, Vector2>
            {
                { Direction.North, new Vector2(0, -stepSize) },
                { Direction.South, new Vector2(0, stepSize) },
                { Direction.East, new Vector2(stepSize, 0) },
                { Direction.West, new Vector2(-stepSize, 0) }
            };

            Direction randomDirection = (Direction)_random.Next(0, 4);
            Vector2 directionOffset = directionOffsets[randomDirection];
            return currentPosition + directionOffset;
        }

    }
}
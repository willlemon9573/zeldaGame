using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.StatePatterns.EnemyStatePatterns;


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
        private double _timeSinceLastAttack;
        private double _attackCooldown = 1.0f;

        /// <summary>
        /// Initializes a new instance of the SmartEnemyMovementController class.
        /// </summary>
        /// <param name="enemyEntity">The enemy entity to be controlled.</param>
        /// <param name="players">List of player entities to interact with.</param>
        /// <param name="remover">Delegate for removing the enemy entity from the game.</param>
        /// <param name="ArchitechtureList">List of architecture entities in the game environment.</param>
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
            _timeSinceLastAttack = 0;
        }

        /// <summary>
        /// Starts the enemy movement control routine.
        /// </summary>
        public void Start()
        {
            _running = true;
        }

        /// <summary>
        /// Stops the enemy movement control routine.
        /// </summary>
        public void Stop()
        {
            _running = false;
        }

        /// <summary>
        /// Calculates the direction of movement based on the given vector.
        /// </summary>
        /// <param name="moveDirection">The vector indicating the direction of movement.</param>
        /// <returns>The calculated direction.</returns>
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

        /// <summary>
        /// Determines whether a boomerang attack should be used based on the positions of the enemy and the player.
        /// </summary>
        /// <param name="enemyPosition">The position of the enemy.</param>
        /// <param name="playerPosition">The position of the player.</param>
        /// <returns>True if a boomerang attack should be used; otherwise, false.</returns>
        private bool ShouldUseBoomerangAttack(Vector2 enemyPosition, Vector2 playerPosition)
        {
            float optimalBoomerangDistance = 50f;

            float distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition);
            return distanceToPlayer <= optimalBoomerangDistance;
        }

        /// <summary>
        /// Updates the state and behavior of the enemy entity based on the game time.
        /// </summary>
        /// <param name="gameTime">The game's current time state.</param>
        public void Update(GameTime gameTime)
        {
            if (_running == false) { return; }
            if (_enemyEntity.Health <= 0)
            {
                _enemyEntity.Die();
                _remove(_enemyEntity);
                Stop();
            }
            if (_enemyEntity is EnemyBasedEntity _enemyBasedEntity)
            {
                if (_enemyBasedEntity.EnemyState is not EnemyMovingState)
                { 
                    return;
                }
            }
            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
            _timeSinceLastPathCalculation += elapsed;
            _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                if (_timeSinceLastAttack >= _attackCooldown)
                {
                    if (_enemyEntity is EnemyBasedEntity enemyBasedEntity)
                    {
                        if (ShouldUseBoomerangAttack(_enemyEntity.Position, nearestPlayer.Position))
                        {
                            Debug.Print("enemyAttacked");
                            enemyBasedEntity.Attack();
                        }
                    }
                    _timeSinceLastAttack = 0;
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
            }

        }

        /// <summary>
        /// Finds the nearest player to the enemy.
        /// </summary>
        /// <param name="enemyPosition">The current position of the enemy.</param>
        /// <param name="players">The list of players in the game.</param>
        /// <returns>The nearest player entity.</returns>
        private IEntity FindNearestPlayer(Vector2 enemyPosition, List<IEntity> players)
        {
            return players.OrderBy(p => Vector2.Distance(enemyPosition, p.Position)).FirstOrDefault();
        }

        /// <summary>
        /// Generates a random direction for the enemy to move in.
        /// </summary>
        /// <param name="currentPosition">The current position of the enemy.</param>
        /// <returns>The new position based on the random direction.</returns>
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
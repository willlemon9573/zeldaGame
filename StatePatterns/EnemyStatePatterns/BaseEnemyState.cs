using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.EnemyStatePatterns
{
    /// <summary>
    /// Abstract parent enemy state, handles default implementations of the enemy states
    /// </summary>
    internal abstract class BaseEnemyState : IEnemyState
    {
        protected EnemyBasedEntity _enemyEntity;
        private readonly Dictionary<State, Func<IEnemyState>> _stateTransitionMap;
        protected EnemySpriteFactory _enemySpriteFactory = EnemySpriteFactory.Instance;
        protected bool _blockTransition = false; // false by default

        /// <summary>
        /// Abstract base enemy constructor.
        /// Note: EnemyEntity already implements its contract - so passing the
        /// EnemyEntity to the constructor is allowed
        /// </summary>
        /// <param name="enemyEntity">The specific EnemyEntity that uses the State Pattern</param>
        public BaseEnemyState(EnemyBasedEntity enemyEntity)
        {
            this._enemyEntity = enemyEntity;
            _stateTransitionMap = new Dictionary<State, Func<IEnemyState>>()
            {
                {State.Moving, () => new EnemyMovingState(_enemyEntity) },
                {State.Attacking, () => new EnemyAttackingState(_enemyEntity) },
                {State.Idle, () => new EnemyIdleState(_enemyEntity) }
                // Add more states as needed
            };
        }


        /// <summary>
        /// Changes the direction of the enemy based on the current state
        /// </summary>
        /// <param name="newDirection">the new direction the enemy will face</param>
        public abstract void ChangeDirection(Direction newDirection);

        public virtual void TransitionState(State newState)
        {
            if (_blockTransition) { return; }
            _enemyEntity.EnemyState = _stateTransitionMap[newState].Invoke();
        }

        /// <summary>
        /// Handles a request made for the enemy
        /// </summary>
        public abstract void Request();

        /// <summary>
        /// Update the enemy based on enemy State. Requires override
        /// </summary>
        /// <param name="gameTime">The current time state of the game</param>
        public abstract void Update(GameTime gameTime);


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /* Check the direction of the player to see if we need to flip
             * the player sprite
             */
            SpriteEffects spriteEffects = _enemyEntity.Direction == Direction.West
                ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            // draw sprite
            _enemyEntity.EnemySprite.Draw(spriteBatch, _enemyEntity.Position, spriteEffects, 0, 0.1f);
        }


        /// <summary>
        /// Blocks enemy from transitioning to a new state. Overridable in case extra logic is required
        /// </summary>
        public virtual void BlockTransition()
        {
            _blockTransition = true;
        }

        /// <summary>
        /// Unblocks enemy state transition. Overridable in case extra logic is required
        /// </summary>
        public virtual void UnblockTranstion()
        {
            _blockTransition = false;
        }
    }
}

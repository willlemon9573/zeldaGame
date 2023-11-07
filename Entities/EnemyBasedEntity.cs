using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.EnemyStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Zihe Wang
    /// </summary>
    internal abstract class BaseEnemyEntity : ICombatEntity, ICollidableEntity
    {
        protected float _totalFrame;
        protected float _attackCooldown;
        protected readonly EnemySpriteFactory _EnemyFactory = EnemySpriteFactory.Instance;
        protected float _timeElapsed;
        protected readonly float _timeToReset = 1f / 7;
        protected ISprite _enemySprite;
        protected Direction _enemyDirection = Direction.South;
        protected float _enemyHealth;
        protected readonly float _enemyHealthMax;
        protected readonly Vector2 _enemyDefaultPosition;
        protected string _enemyName;
        protected Vector2 _enemyPosition;
        protected IEnemyState _enemyState;
        public ISprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }
        public string EnemyName { get { return _enemyName; } set { _enemyName = value; } }
        public Vector2 Position { get { return _enemyPosition; } set { _enemyPosition = value; _collider.Update(this); } }
        public float Health { get { return _enemyHealth; } set { _enemyHealth = value; } }
        public Direction Direction { get { return _enemyDirection; } set { _enemyDirection = value; } }
        public IEnemyState EnemyState { get { return _enemyState; } set { _enemyState = value; } }
        private ICollider _collider;
        public ICollider Collider { get { return _collider; } }


        /// <summary>
        /// Base abstract enemy constructor
        /// </summary>
        /// <param name="enemySprite">The sprite the entity will use</param>
        /// <param name="startingPosition">The starting position of the entity</param>
        /// <param name="startingHealth">the starting health of the entity</param>
        /// <param name="enemyName">the name of the entity</param>
        protected BaseEnemyEntity(ISprite enemySprite, Vector2 startingPosition, float startingHealth, string enemyName)
        {
            _enemyHealth = startingHealth;
            _enemyHealthMax = startingHealth;
            _enemyDefaultPosition = startingPosition;
            _enemyPosition = startingPosition;
            _enemyName = enemyName;
            _enemyState = new EnemyIdleState(this);
            _collider = new DynamicCollider(new Rectangle((int)startingPosition.X, (int)startingPosition.Y, 16, 16));
        }

        public void Reset()
        {
            _enemyPosition = _enemyDefaultPosition;
            _enemyHealth = _enemyHealthMax;
        }

        public virtual void Move()
        {
            if (_enemyState is not EnemyMovingState) { TransitionToState(State.Moving); }
            _enemyState.Request();
        }

        public virtual void TransitionToState(State newState)
        {
            _enemyState.TransitionState(newState);
        }

        public virtual void Attack(string weaponName)
        {
            PerformAttack();
        }

        public abstract void PerformAttack();

        public virtual void TakeDamage(int damage)
        {
            /* _enemyHealth -= damage;
             if (_enemyHealth <= 0)
             {
                 Die();
             }*/
        }

        public virtual void Die()
        {
            // not implemented yet
        }

        public virtual void ChangeDirection(Direction direction)
        {
            _enemyState.ChangeDirection(direction);
        }
        //_enemyDirection = direction;

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

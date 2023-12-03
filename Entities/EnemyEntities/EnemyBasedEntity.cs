using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.EnemyStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;


namespace SprintZero1.Entities.EnemyEnetities
{
    /// <summary>
    /// EnemyBasedEntity class: An abstract class used to control and update enemy entities.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal abstract class EnemyBasedEntity : ICombatEntity, ICollidableEntity

    {
        protected float _totalFrame;
        protected float _attackCooldown;
        protected int _enemyHealthMax;
        protected Vector2 _enemyDefaultPosition;
        protected readonly EnemySpriteFactory _EnemyFactory = EnemySpriteFactory.Instance;
        protected readonly float _timeToReset = 1f / 7;
        protected ISprite _enemySprite;
        protected Direction _enemyDirection = Direction.South;
        protected float _enemyHealth;
        protected string _enemyName;
        protected Vector2 _enemyPosition;
        protected IEnemyState _enemyState;
        protected ICollider _collider;
        protected SoundEffect _deathSound;
        protected SoundEffect _damageSound;
        public ISprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }
        public string EnemyName { get { return _enemyName; } set { _enemyName = value; } }
        public Vector2 Position { get { return _enemyPosition; } set { _enemyPosition = value; _collider.Update(this); } }
        public float Health { get { return _enemyHealth; } set { _enemyHealth = value; } }
        public Direction Direction { get { return _enemyDirection; } set { _enemyDirection = value; } }
        public IEnemyState EnemyState { get { return _enemyState; } set { _enemyState = value; } }
        public ICollider Collider { get { return _collider; } }

        /// <summary>
        /// Constructs a new enemy entity.
        /// </summary>
        /// <param name="position">The position of the enemy entity.</param>
        /// <param name="startingHealth">The starting health of the enemy entity.</param>
        /// <param name="enemyName">The name of the enemy.</param>
        protected EnemyBasedEntity(Vector2 position, int startingHealth, string enemyName)
        {
            _enemyHealthMax = startingHealth;
            _enemyDefaultPosition = position;
            ResetEnemy(); /* why? */
            _enemyName = enemyName;
            _enemyState = new EnemyIdleState(this);
            _enemySprite = _EnemyFactory.CreateEnemySprite(enemyName, _enemyDirection);
            _collider = new EnemyCollider(position, new System.Drawing.Size(_enemySprite.Width, _enemySprite.Height));
            _deathSound = SoundFactory.GetSound("enemy_death");
            _damageSound = SoundFactory.GetSound("enemy_hit");
        }

        public void ResetEnemy()
        {
            _enemyHealth = _enemyHealthMax;
            _enemyPosition = _enemyDefaultPosition;
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

        public virtual void Attack()
        {
            PerformAttack();
        }

        public abstract void PerformAttack();

        public virtual void TakeDamage(float damage)
        {
            _enemyHealth -= damage;
            _damageSound.Play();
        }

        public void PauseEnemy()
        {
            TransitionToState(State.Paused);
            _enemyState.Request();
        }

        public virtual void Die()
        {
            _deathSound.Play();
        }

        public virtual void ChangeDirection(Direction direction)
        {
            _enemyState.ChangeDirection(direction);
        }

        public virtual void Update(GameTime gameTime)
        {
            _enemyState.Update(gameTime);
            _collider.Update(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _enemyState.Draw(spriteBatch);
        }
    }
}

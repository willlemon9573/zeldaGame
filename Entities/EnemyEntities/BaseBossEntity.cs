using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities.EnemyEntities
{
    internal abstract class BaseBossEntity : ICombatEntity, ICollidableEntity
    {
        private const string BossDeathSound = "boss_scream";
        private const string BossDamageSound = "boss_hit";
        protected const float DefaultTouchDamage = 1f;
        protected readonly float MaxHealth;
        protected readonly SoundEffect _bossDamageSound;
        protected readonly SoundEffect _bossDeathSound;
        protected float _currentHealth;
        protected ISprite _bossSprite;
        protected Direction _currentDirection;
        protected Vector2 _currentPosition;
        protected ICollider _bossCollider;
        protected IEnemyState _bossState;

        public float Health { get { return _currentHealth; } set { _currentHealth = value; } }
        public Direction Direction { get { return _currentDirection; } set { _currentDirection = value; } }
        public Vector2 Position { get { return _currentPosition; } set { _currentPosition = value; } }

        public ICollider Collider { get { return _bossCollider; } }

        public float TouchDamage { get { return DefaultTouchDamage; } }

        protected BaseBossEntity(float startingHealth, Direction startingDirection, Vector2 startingPosition)
        {
            MaxHealth = startingHealth;
            Direction = startingDirection;
            Position = startingPosition;
            _currentHealth = startingHealth;
            _bossDeathSound = SoundFactory.GetSound(BossDeathSound);
            _bossDamageSound = SoundFactory.GetSound(BossDamageSound);
        }

        /// <summary>
        /// Used to transition states. Overridable.
        /// </summary>
        /// <param name="newState">The new state to transition to</param>
        protected virtual void TransitionState(State newState)
        {
            _bossState.TransitionState(newState);
        }

        public abstract void Attack();
        public abstract void ChangeDirection(Direction newDirection);

        public abstract void Die();

        public abstract void Move();

        public abstract void TakeDamage(float damage);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}

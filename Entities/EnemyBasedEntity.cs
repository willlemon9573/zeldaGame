using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.StatePatterns.EnemyStatePatterns;
//using SprintZero1.StatePatterns.CombatStatePatterns;
//using SprintZero1.StatePatterns.MovingStatePatterns;
//using SprintZero1.StatePatterns.StatePatternInterfaces;


namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Zihe Wang
    /// </summary>
    internal abstract class EnemyBasedEntity : ICombatEntity, ICollidableEntity
    {
        //Enemy Components
        //protected IProjectileEntity projectileSprite;
        protected int _totalFrame;
        protected float _attackCooldown;
        //protected EnemyCollider _enemyCollider;
        //protected readonly EnemyStateMachine _enemyStateMachine;
        protected readonly EnemySpriteFactory _EnemyFactory = EnemySpriteFactory.Instance; // will be removed to give player a sprite on instantiation 
        //controls the attacking state
        protected float _timeElapsed;
        //protected readonly string _weapon;
        protected readonly float _timeToReset = 1f / 7;
        protected ISprite _enemySprite;
        public ISprite EnemySprite { get { return _enemySprite; } set { _enemySprite = value; } }
        protected string _enemyName;
        public string EnemyName { get { return _enemyName; } set { _enemyName = value; } }

        protected Vector2 _enemyPosition;
        public Vector2 Position { get { return _enemyPosition; } set { _enemyPosition = value; _collider.Update(this); } }

        protected int _enemyHealth;
        public int Health { get { return _enemyHealth; } set { _enemyHealth = value; } }

        protected Direction _enemyDirection = Direction.South;
        public Direction Direction { get { return _enemyDirection; } set { _enemyDirection = value; } }

        protected IEnemyState _enemyState;
        public IEnemyState EnemyState { get { return _enemyState; } set { _enemyState = value; } }

        private ICollider _collider;
        public ICollider Collider { get { return _collider; } }

        /*protected IMovingEntityState _enemyMovingState;
        public IMovingEntityState State { get { return _playerStates.Item1; } set {; } }*/

        /*        private void Reset(float deltaTime)
                {
                    _timeElapsed += deltaTime;
                    if (_timeElapsed >= _timeToReset)
                    {
                        _enemySprite = _EnemyFactory.GetLinkSprite(_playerDirection);
                        _enemyStateMachine.ChangeState(State.Idle);
                        _enemyStateMachine.UnblockTransition();
                        _timeElapsed = 0f;
                    }
                }*/

        /// <summary>
        /// Construct a new enemy entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        protected EnemyBasedEntity(Vector2 position, int startingHealth, string enemyName,bool isBoss = false)
        {
            _enemyHealth = startingHealth;
            _enemyPosition = position;
            _enemyName = enemyName;
            _enemyState = new EnemyIdleState(this);
            _collider = new DynamicCollider(new Rectangle((int)position.X, (int)position.Y, 16, 16));
            _enemySprite = !isBoss ? _EnemyFactory.CreateEnemySprite(enemyName, _enemyDirection) : _EnemyFactory.CreateBossSprite(enemyName, _enemyDirection);

        }

        
        public virtual void Move()
        {
            if (_enemyState is not EnemyMovingState) { TransitionToState(State.Moving); }
            _enemyState.Request();
        }
            /*if (_playerState is not PlayerMovingState) { TransitionToState(State.Moving); }
            _playerState.Request();*/
        

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

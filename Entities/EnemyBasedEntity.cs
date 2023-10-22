using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;
using SprintZero1.StatePatterns.CombatStatePatterns;
using SprintZero1.StatePatterns.MovingStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Zihe Wang
    /// </summary>
    internal abstract class EnemyBasedEntity : ICombatEntity
    {
        /* Enemy Components */
        protected ProjectileEntity projectileSprite;
        protected ISprite _enemySprite;
        protected int _totalFrame;
        protected string _enemyName;
        protected float _attackCooldown;
        //protected EnemyCollider _enemyCollider;
        protected readonly EnemyStateMachine _enemyStateMachine;
        protected readonly EnemySpriteFactory _EnemyFactory = EnemySpriteFactory.Instance; // will be removed to give player a sprite on instantiation 
        /* controls the attacking state */
        protected float _timeElapsed;
        protected readonly string _weapon;
        protected readonly float _timeToReset = 1f / 7;

        protected IMovableEntity _enemyMainWeapon ;

        protected Vector2 _enemyPosition;
        public Vector2 Position { get { return _enemyPosition; } set { _enemyPosition = value; } }

        protected int _enemyHealth;
        public int Health { get { return _enemyHealth; } set { _enemyHealth = value; } }

        protected Direction _enemyDirection = Direction.South;
        public Direction Direction { get { return _enemyDirection; } }

        protected IMovingEntityState _enemyMovingState;
        public IMovingEntityState State { get { return _playerStates.Item1; } set {; } }

        /*private void Reset(float deltaTime)
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
        public EnemyBasedEntity()
        {
            projectileSprite = new ProjectileEntity();
        }
        //Vector2 position, int startingHealth, string enemyName, int totalFrames
        /*_enemyHealth = startingHealth;
            _enemyPosition = position;
            //_enemyStateMachine = new PlayerStateMachine(State.Idle);
            _enemySprite = _linkSpriteFactory.GetLinkSprite(enemyName, totalFrames);
            _enemyName = enemyName;
            //_enemyCollider = new enemyCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16));
        */

        public virtual void Move(Vector2 distance)
        {
            _enemyPosition += distance;
        }

        public virtual void Attack(string weaponName)
        {
            PerformAttack();
        }

        public abstract void PerformAttack();


        public virtual void TakeDamage(int damage)
        {
            /*_enemyHealth -= damage;
            if (_enemyHealth <= 0)
            {
                Die();
            }*/
        }

        public virtual void Die()
        {
            // not implemented yet
        }

        public abstract void ChangeDirection(Direction direction);
        
        

        public void Update(GameTime gameTime)
        {
            _enemyMovingState.Update(gameTime);
            if (_enemyMovingState is not IdleMovingState)
            {
                _enemySprite.Update(gameTime);
            }
            //_playerCollider.Update(gameTime);
            /*var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _attackCooldown -= deltaTime; 
            bool canTransition = _enemyStateMachine.CanTransition();
            State currentState = _enemyStateMachine.GetCurrentState();
            
            if (currentState == State.Moving || currentState == State.Attacking)
            {
                *//* Sprite only updates when player is moving / attacking *//*
                _enemyMainWeapon?.Update(gameTime);
                _enemySprite.Update(gameTime);
            }

            if (currentState == State.Attacking)
            {
                Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
            }*/
            //_playerCollider.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            
            if (_enemyDirection == Direction.West)
            {
                /* Considering adding this as an option for creating a sprite so it doesn't have to be called each time */
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            projectileSprite?.Draw(spriteBatch);
            _enemySprite.Draw(spriteBatch, _enemyPosition, spriteEffects);
        }
    }
}

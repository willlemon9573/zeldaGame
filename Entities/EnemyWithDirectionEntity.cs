using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.StateMachines;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Player Entity class used to control and update player.
    /// @Author Zihe Wang
    /// </summary>
    internal abstract class EnemyWithDirectionEntity : IEntity, IMovableEntity, ICombatEntity, IDamageableEntity
    {
        /* Enemy Components */
        private string _enemyName;
        private float _attackCooldown;
        private ISprite _enemySprite;
        private EnemyCollider _enemyCollider;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyFactory _EnemyFactory = EnemyFactory.Instance; // will be removed to give player a sprite on instantiation 
        /* controls the attacking state */
        private float _timeElapsed;
        private readonly string _weapon;
        private readonly float _timeToReset = 1f / 7;

        private ProjectileEntity _enemyMainWeapon ;

        private Vector2 _enemyPosition;
        public Vector2 Position { get { return _enemyPosition; } set { _enemyPosition = value; } }

        private int _enemyHealth;
        public int Health { get { return _enemyHealth; } set { _enemyHealth = value; } }

        private Direction _enemyDirection = Direction.South;
        public Direction Direction { get { return _enemyDirection; } }

        private void Reset(float deltaTime)
        {
            _timeElapsed += deltaTime;
            if (_timeElapsed >= _timeToReset)
            {
                _playerSprite = _linkSpriteFactory.GetLinkSprite(_playerDirection);
                _enemyStateMachine.ChangeState(State.Idle);
                _enemyStateMachine.UnblockTransition();
                _timeElapsed = 0f;
            }
        }

        /// <summary>
        /// Construct a new enemy entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public EnemyWithDirectionEntity(Vector2 position, int startingHealth, string enemyName, int totalFrames)
        {
            _enemyHealth = startingHealth;
            _enemyPosition = position;
            //_enemyStateMachine = new PlayerStateMachine(State.Idle);
            _enemySprite = _linkSpriteFactory.GetLinkSprite(enemyName, totalFrames);
            _enemyName = enemyName;
            //_enemyCollider = new enemyCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16));
            _enemyMainWeapon = new ProjectileEntity();

        }

        public virtual void Move(Vector2 distance)
        {
            //wait for state machine
        }

        public virtual void Attack(string weaponName)
        {
            PerformAttack();
        }

        private abstract void PerformAttack();
        /*
            if (_enemyName.Equals("dungeon_zol"))
            {
                FireBoomerangCommand fireBoomerangCommand = new FireBoomerangCommand(this, boomerangEntity);
                fireBoomerangCommand.Execute();
            }
            else if(_enemyName.Equals("aquamentus"))
            {

            }
         */


        public virtual void TakeDamage()
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

        public virtual void ChangeDirection(Direction direction)
        {
            //wait for state machine
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _attackCooldown -= deltaTime; 
            bool canTransition = _enemyStateMachine.CanTransition();
            State currentState = _enemyStateMachine.GetCurrentState();
            
            if (currentState == State.Moving || currentState == State.Attacking)
            {
                /* Sprite only updates when player is moving / attacking */
                _enemyMainWeapon?.Update(gameTime);
                _enemySprite.Update(gameTime);
            }

            if (currentState == State.Attacking)
            {
                Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
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
            _enemyMainWeapon?.Draw(spriteBatch);
            _enemySprite.Draw(spriteBatch, _playerPosition, spriteEffects);
        }
    }
}

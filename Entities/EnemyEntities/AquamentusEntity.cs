using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern;

namespace SprintZero1.Entities.EnemyEnetities
{
    internal class AquamentusEntity : BaseBossEntity
    {
        const string BossName = "aquamentus";
        private AquamentusBossController _bossController;
        public AquamentusEntity(float startingHealth, Direction startingDirection, Vector2 startingPosition, Rectangle boundary, RemoveDelegate remover) : base(startingHealth, startingDirection, startingPosition)
        {
            // all other values inherited from the BaseBossEntity to prevent duplicate code
            _bossSprite = EnemySpriteFactory.Instance.CreateBossSprite(BossName);
            int bossColliderOffSetX = -5;
            int bossColliderOffSetY = -5;
            _bossCollider = new BossCollider(startingPosition, new System.Drawing.Size(_bossSprite.Width, _bossSprite.Height), bossColliderOffSetX, bossColliderOffSetY);
            _currentState = new AquamentusMovingState(this);
            _vulnerabilityState = new AquamentusVulnerabilityState(this);
            _bossController = new AquamentusBossController(this, remover, boundary);
            _currentDirection = Direction.East;
        }

        public override void Attack()
        {
            if (_currentState is not AquamentusAttackingState) { TransitionState(State.Attacking); }
            _currentState.Request();
        }

        public override void ChangeDirection(Direction newDirection)
        {
            if (_currentDirection == newDirection) { return; }
            _currentDirection = newDirection;
        }

        public override void Die()
        {
            if (_currentHealth <= 0 && _currentState is not AquamentusDeathState) { TransitionState(State.Attacking); }
            _currentState.Request();
        }

        public override void Move()
        {
            if (_currentState is not AquamentusMovingState) { TransitionState(State.Moving); }
            _currentState.Request();
        }

        public override void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
                return;
            }
            _vulnerabilityState.Request();
            _bossDamageSound.Play();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _vulnerabilityState.Draw(spriteBatch);
            _currentState.Draw(spriteBatch);
        }


        public override void Update(GameTime gameTime)
        {
            _bossController.Update(gameTime);
            _vulnerabilityState.Update(gameTime);
            _currentState.Update(gameTime);
            _bossCollider.Update(this);
        }
    }
}

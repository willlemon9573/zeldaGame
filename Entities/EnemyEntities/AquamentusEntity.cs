using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders.EntityColliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern;

namespace SprintZero1.Entities.EnemyEnetities
{
    internal class AquamentusEntity : BaseBossEntity
    {
        const string BossName = "aquamentus";
        public AquamentusEntity(float startingHealth, Direction startingDirection, Vector2 startingPosition) : base(startingHealth, startingDirection, startingPosition)
        {
            // all other values inherited from the BaseBossEntity to prevent duplicate code
            _bossSprite = EnemySpriteFactory.Instance.CreateBossSprite(BossName);
            _bossCollider = new BossCollider(startingPosition, new System.Drawing.Size(_bossSprite.Width, _bossSprite.Height));
            _currentState = new AquamentusMovingState(this);
        }

        public override void Attack()
        {

        }

        public override void ChangeDirection(Direction newDirection)
        {

        }

        public override void Die()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Move()
        {

        }

        public override void TakeDamage(float damage)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}

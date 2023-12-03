using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;

namespace SprintZero1.Entities.EnemyEnetities
{
    internal class AquamentusEntity : BaseBossEntity
    {
        const string BossName = "aquamentus";
        public AquamentusEntity(float startingHealth, Direction startingDirection, Vector2 startingPosition) : base(startingHealth, startingDirection, startingPosition)
        {
            // constructor values set in parent class
            _bossSprite = EnemySpriteFactory.Instance.CreateBossSprite(BossName);
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

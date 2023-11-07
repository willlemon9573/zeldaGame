using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{

    internal class EnemyEntityWithoutDirection : BaseEnemyEntity
    {
        public EnemyEntityWithoutDirection(ISprite enemySprite, Vector2 startingPosition, int startingHealth, string enemyName) : base(enemySprite, startingPosition, startingHealth, enemyName)
        {
        }

        public override void PerformAttack()
        {
            //do this later
        }
        public override void Update(GameTime gameTime)
        {
            _enemyState.Update(gameTime);
            //projectileSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _enemyState.Draw(spriteBatch);
        }
    }
}

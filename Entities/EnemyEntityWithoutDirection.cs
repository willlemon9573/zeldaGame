using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Entities
{

    internal class EnemyEntityWithoutDirection : EnemyBasedEntity
    {
        public EnemyEntityWithoutDirection(Vector2 startingPosition, int startingHealth, string enemyName) : base(startingPosition, startingHealth, enemyName)
        {
        }

        public override void PerformAttack()
        {
            //do this later
        }
        public override void Update(GameTime gameTime)
        {
            this._collider.Update(this);
            _enemyState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _enemyState.Draw(spriteBatch);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{

    internal class EnemyEntityWithDirection : EnemyBasedEntity
    {
        public EnemyEntityWithDirection(Vector2 startingPosition, int startingHealth, string enemyName) : base( startingPosition, startingHealth, enemyName)
        {
        }

        public override void PerformAttack()
        {

        }

        public override void Update(GameTime gameTime)
        {
            _enemySprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (_enemyDirection == Direction.West)
            {
                //Considering adding this as an option for creating a sprite so it doesn't have to be called each time 

                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            //projectileSprite?.Draw(spriteBatch);
            _enemySprite.Draw(spriteBatch, _enemyPosition, spriteEffects);
        }
    }
}

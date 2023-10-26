using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.Commands;
//using SprintZero1.StatePatterns.CombatStatePatterns;
//using SprintZero1.StatePatterns.MovingStatePatterns;
//using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.Controllers.EnemyControllers;

namespace SprintZero1.Entities
{

    internal class EnemyEntityWithoutDirection : EnemyBasedEntity
    {

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public EnemyEntityWithoutDirection(Vector2 position, int startingHealth, string enemyName, bool isBoss = false)
        : base(position, startingHealth, enemyName, isBoss)
        {
            //no special constructor thing
           
        }


        public override void PerformAttack()
        {
            //do this later
        }
        public override void Update(GameTime gameTime)
        {
            _enemyState.Update(gameTime);
            projectileSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _enemyState.Draw(spriteBatch);
        }
    }
}

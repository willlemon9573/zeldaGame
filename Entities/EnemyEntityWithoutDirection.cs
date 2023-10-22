using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.Commands;
using SprintZero1.StatePatterns.CombatStatePatterns;
using SprintZero1.StatePatterns.MovingStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;

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
        public EnemyEntityWithoutDirection(Vector2 position, int startingHealth, string enemyName, int totalFrames, bool isBoss = false)
        : base(position, startingHealth, enemyName, totalFrames, isBoss)
        {
            //no special constructor thing
        }

        public override void ChangeDirection(Direction direction)
		{
            //nothing need to be done
            throw new NotSupportedException("ChangeDirection is not supported by this class.");
        }

		public override void PerformAttack()
		{
            //do this later
		}
        public override void Update(GameTime gameTime)
        {
            _enemyMovingState.Update(gameTime);
            if (_enemyMovingState is not IdleMovingState)
            {
                _enemySprite.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            _enemySprite.Draw(spriteBatch, _enemyPosition, spriteEffects);
        }
    }
}

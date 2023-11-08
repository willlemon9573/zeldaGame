using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using SprintZero1.StatePatterns.CombatStatePatterns;
//using SprintZero1.StatePatterns.MovingStatePatterns;
//using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{

    internal class EnemyEntityWithoutProjectile : EnemyBasedEntity
    {

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public EnemyEntityWithoutProjectile(Vector2 position, int startingHealth, string enemyName)
        : base(position, startingHealth, enemyName)
        {
            //no special constructor thing

        }


        public override void PerformAttack()
        {
            //do this later
        }

    }
}

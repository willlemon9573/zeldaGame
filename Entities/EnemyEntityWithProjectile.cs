using Microsoft.Xna.Framework;
//using SprintZero1.StatePatterns.CombatStatePatterns;
//using SprintZero1.StatePatterns.MovingStatePatterns;
//using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Represents an enemy entity that has projectile attack capabilities.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class EnemyEntityWithProjectile : EnemyBasedEntity
    {
        /// <summary>
        /// Constructs a new enemy entity with projectile capabilities.
        /// </summary>
        /// <param name="position">The position of the enemy entity.</param>
        /// <param name="startingHealth">The starting health of the enemy entity.</param>
        /// <param name="enemyName">The name of the enemy.</param>
        public EnemyEntityWithProjectile(Vector2 position, int startingHealth, string enemyName)
        : base(position, startingHealth, enemyName)
        {
            //no special constructor thing
        }



        public override void PerformAttack()
        {
            /*if (_enemyName.Equals("dungeon_zol"))
            {
                ICommand fireBoomerangCommand = new FireBoomerangCommand(this, projectileSprite);
                fireBoomerangCommand.Execute();
            }
            else if (_enemyName.Equals("aquamentus"))
            {
                ICommand FireAquamentusWeaponCommand = new FireAquamentusWeaponCommand(this, projectileSprite);
                FireAquamentusWeaponCommand.Execute();
            }*/
        }

    }
}

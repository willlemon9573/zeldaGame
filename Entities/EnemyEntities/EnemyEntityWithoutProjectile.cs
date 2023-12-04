﻿using Microsoft.Xna.Framework;

namespace SprintZero1.Entities.EnemyEnetities
{
    /// <summary>
    /// Represents an enemy entity that does not have a projectile attack.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class EnemyEntityWithoutProjectile : EnemyBasedEntity
    {
        /// <summary>
        /// Constructs a new enemy entity without projectile capabilities.
        /// </summary>
        /// <param name="position">The position of the enemy entity.</param>
        /// <param name="startingHealth">The starting health of the enemy entity.</param>
        /// <param name="enemyName">The name of the enemy.</param>
        public EnemyEntityWithoutProjectile(Vector2 position, float startingHealth, string enemyName)
            : base(position, startingHealth, enemyName)
        {
            // No specific construction logic required
        }

        public override void PerformAttack()
        {
            // Attack implementation will be added later
        }
    }
}

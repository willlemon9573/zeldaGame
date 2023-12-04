using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.EnemyStatePatterns;
//using SprintZero1.StatePatterns.CombatStatePatterns;
//using SprintZero1.StatePatterns.MovingStatePatterns;
//using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.Entities.EnemyEntities
{
    /// <summary>
    /// Represents an enemy entity that does not have a projectile attack.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class EnemyEntityWithProjectile : EnemyBasedEntity
    {

        private IWeaponEntity _EnemyWeapon;
        /// <summary>
        /// Constructs a new enemy entity without projectile capabilities.
        /// </summary>
        /// <param name="position">The position of the enemy entity.</param>
        /// <param name="startingHealth">The starting health of the enemy entity.</param>
        /// <param name="enemyName">The name of the enemy.</param>
        public EnemyEntityWithProjectile(Vector2 position, float startingHealth, string enemyName)
        : base(position, startingHealth, enemyName)
        {
            _EnemyWeapon = new EnemyBoomerangEntity("boomerang", this);
        }

        public override void PerformAttack()
        {
            if (_enemyState is not EnemyAttackingState)
            {
                TransitionToState(State.Attacking);
            }
            _EnemyWeapon.UseWeapon(_enemyDirection, _enemyPosition);
            _enemyState.Request();
        }
        public override void Update(GameTime gameTime)
        {
            _enemyState.Update(gameTime);
            _EnemyWeapon.Update(gameTime);
            _collider.Update(this);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _enemyState.Draw(spriteBatch);
            _EnemyWeapon.Draw(spriteBatch);
        }
    }
}

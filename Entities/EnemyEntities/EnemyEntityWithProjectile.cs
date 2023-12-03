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
        /// <summary>
        /// Constructs a new enemy entity without projectile capabilities.
        /// </summary>
        /// <param name="position">The position of the enemy entity.</param>
        /// <param name="startingHealth">The starting health of the enemy entity.</param>
        /// <param name="enemyName">The name of the enemy.</param>
        IWeaponEntity _EnemyWeapon;
        string weaponName = "Boomerang";
        private float _timeSinceLastAttack = 0f;
        private const float AttackCooldownTime = 3f;
        public EnemyEntityWithProjectile(Vector2 position, int startingHealth, string enemyName)
            : base(position, startingHealth, enemyName)
        {
            _EnemyWeapon = new RegularBoomerangEntity("Boomerang", this);
            // No specific construction logic required
            _attackCooldown = AttackCooldownTime;
        }

        public override void PerformAttack()
        {
            if (_timeSinceLastAttack >= _attackCooldown)
            {
                if (_enemyState is not EnemyAttackingState)
                {
                    TransitionToState(State.Attacking);
                }

                if (weaponName == "Boomerang")
                {
                    _EnemyWeapon.UseWeapon(_enemyDirection, _enemyPosition);
                }
                _enemyState.Request();

                _timeSinceLastAttack = 0f;
            }
        }
        public override void Update(GameTime gameTime)
        {
            _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
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

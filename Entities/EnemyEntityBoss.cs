using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.BoomerangEntity;
using SprintZero1.Enums;

namespace SprintZero1.Entities
{
	internal class EnemyEntityBoss : EnemyBasedEntity
	{
		private IWeaponEntity _bossWeapon;
		private float _timeSinceLastAttack = 0f;
		private readonly float _attackCooldown = 3f; 

		public EnemyEntityBoss(Vector2 position, int startingHealth, string enemyName)
			: base(position, startingHealth, enemyName)
		{
			_enemySprite = new CreateBossSprite("aquamentus");
			_bossWeapon = new RegularBoomerangEntity("Boomerang", this);
		}

		public override void PerformAttack()
		{
			if (_timeSinceLastAttack >= _attackCooldown)
			{
				if (_enemyState is not EnemyAttackingState)
				{
					TransitionToState(State.Attacking);
				}

				_bossWeapon.UseWeapon(_enemyDirection, _enemyPosition);
				_enemyState.Request();

				_timeSinceLastAttack = 0f;
			}
		}

		public override void Update(GameTime gameTime)
		{
			_timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
			_enemyState.Update(gameTime);
			_bossWeapon.Update(gameTime); 
			_collider.Update(this);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			_enemyState.Draw(spriteBatch);
			_bossWeapon.Draw(spriteBatch); 
		}
	}
}

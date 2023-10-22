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

	internal class EnemyEntityWithDirection : EnemyBasedEntity
	{ 

		/// <summary>
		/// Construct a new player entity
		/// </summary>
		/// <param name="position">The position of the player entity</param>
		/// <param name="startingHealth">The starting health of the player entity</param>
		/// <param name="startingDirection">The starting direction the player entity will be facing</param>
		public EnemyEntityWithDirection(Vector2 position, int startingHealth, string enemyName, int totalFrames):base()
		{
			/* Default values for player upon game start */
			_totalFrame = totalFrames;
			_enemyHealth = startingHealth;
			_enemyPosition = position;
			//_enemyStateMachine = new PlayerStateMachine(State.Idle);
			_enemySprite = _EnemyFactory.CreateEnemySprite(enemyName, totalFrames);
			_enemyName = enemyName;
			//_enemyCollider = new enemyCollider(this, new Rectangle((int)Position.X, (int)Position.Y, 16, 16));
		}

		public override void ChangeDirection(Direction direction)
		{
			_enemyMovingState.ChangeDirection(direction);
			_enemySprite = _EnemyFactory.CreateEnemySprite(_enemyName, _totalFrame);
			_enemyDirection = direction;
		}
		public override void PerformAttack()
		{
			if (_enemyName.Equals("dungeon_zol"))
			{
				FireBoomerangCommand fireBoomerangCommand = new FireBoomerangCommand(this, projectileSprite);
				fireBoomerangCommand.Execute();
			}
			else if (_enemyName.Equals("aquamentus"))
			{

			}
		}
	}
}

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

    internal class EnemyEntityWithDirection : EnemyBasedEntity
    {

        /// <summary>
        /// Construct a new player entity
        /// </summary>
        /// <param name="position">The position of the player entity</param>
        /// <param name="startingHealth">The starting health of the player entity</param>
        /// <param name="startingDirection">The starting direction the player entity will be facing</param>
        public EnemyEntityWithDirection(Vector2 position, int startingHealth, string enemyName, bool isBoss = false)
        : base(position, startingHealth, enemyName, isBoss)
        {
            //no special constructor thing
        }



        public override void PerformAttack()
        {
            if (_enemyName.Equals("dungeon_zol"))
            {
                ICommand fireBoomerangCommand = new FireBoomerangCommand(this, projectileSprite);
                fireBoomerangCommand.Execute();
            }
            else if (_enemyName.Equals("aquamentus"))
            {
                ICommand FireAquamentusWeaponCommand = new FireAquamentusWeaponCommand(this, projectileSprite);
                FireAquamentusWeaponCommand.Execute();
            }
        }
        public override void Update(GameTime gameTime)
        {
            _enemySprite.Update(gameTime);
            projectileSprite.Update(gameTime);
            /* _enemyMovingState.Update(gameTime);
             if (_enemyMovingState is not IdleMovingState)
             {
                 projectileSprite.Update(gameTime);
                 _enemySprite.Update(gameTime);
             }
             //_playerCollider.Update(gameTime);
             var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
             _attackCooldown -= deltaTime;
             bool canTransition = _enemyStateMachine.CanTransition();
             State currentState = _enemyStateMachine.GetCurrentState();

             if (currentState == State.Moving || currentState == State.Attacking)
             {
                 //Sprite only updates when player is moving / attacking
                 _enemyMainWeapon?.Update(gameTime);
                 _enemySprite.Update(gameTime);
             }

             if (currentState == State.Attacking)
             {
                 Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
             }*/
            //_playerCollider.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (_enemyDirection == Direction.West)
            {
                //Considering adding this as an option for creating a sprite so it doesn't have to be called each time 

               spriteEffects = SpriteEffects.FlipHorizontally;
            }
            projectileSprite.Draw(spriteBatch);
            _enemySprite.Draw(spriteBatch, _enemyPosition, spriteEffects);
        }
    }
}

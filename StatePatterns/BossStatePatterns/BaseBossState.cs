using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EnemyEntities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.StatePatterns.BossStatePatterns
{
    internal abstract class BaseBossState : IEnemyState
    {

        public BaseBossState(BaseBossEntity boss)
        {

        }


        public void BlockTransition()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDirection(Direction newDirection)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void TransitionState(State newState)
        {
            throw new System.NotImplementedException();
        }

        public void UnblockTranstion()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

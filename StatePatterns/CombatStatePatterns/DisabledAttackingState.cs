using Microsoft.Xna.Framework;
using SprintZero1.Entities;
namespace SprintZero1.StatePatterns.CombatStatePatterns
{
    internal class DisabledAttackingState : BaseAttackingState
    {
        public DisabledAttackingState(IEntity entity)
        {

        }
        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

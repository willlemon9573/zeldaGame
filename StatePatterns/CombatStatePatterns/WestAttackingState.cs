using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.CombatStatePatterns
{
    internal class WestAttackingState : BaseAttackingState
    {
        public WestAttackingState(IEntity entity)
        {

        }
        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

using SprintZero1.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.AI
{
    internal class EnemyAI : IAI
    {
        Enemy character;

        public EnemyAI(Enemy character)
        { this.character = character; }

        public void Update()
        {

        }
    }
}

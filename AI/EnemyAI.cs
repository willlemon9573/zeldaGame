using Microsoft.Xna.Framework;
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
        float fireTimer = 0;
        int up = 1;

        public EnemyAI(Enemy character)
        { 
            this.character = character;
        }

        public void Update(GameTime timer)
        {
            character.pos.Y += 1 * up;
            if(character.pos.Y > 300 || character.pos.Y < 100 ) { up = -up; }
            fireTimer += (float) timer.ElapsedGameTime.TotalSeconds;
            if (fireTimer > 2)
            {
                Fire();
                fireTimer = 0;
            }
        }

        public void Fire()
        {
            character.Fire();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}

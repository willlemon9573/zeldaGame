using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class StrikeEnemyCommand : ICommand
    {
        EnemyEntityWithoutDirection enemy;
        SwordEntity sword;
        public StrikeEnemyCommand(SwordEntity sword, EnemyEntityWithoutDirection enemy) 
        {
            this.sword = sword;
            this.enemy = enemy;
        }

        public void Execute()
        {
            enemy.TakeDamage(2);
        }
    }
}

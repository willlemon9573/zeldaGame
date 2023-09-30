﻿using SprintZero1.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Characters;

namespace SprintZero1.Commands
{
    internal class ChangeEnemyCommand : ICommand
    {
        private readonly List<string> enemyNames;
        private readonly Game1 myGame;
        private readonly ISpriteFactory enemySpriteFactory;
        private int totalEnemies;
        EnemyFactory enemyFactory;
        Enemy character;
        public ChangeEnemyCommand(Game1 game)
        {
            myGame = game;
            character = game.enemy;
        }

        public void Execute()
        {
            //
        }
    }
}

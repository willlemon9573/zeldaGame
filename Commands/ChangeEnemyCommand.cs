using SprintZero1.Characters;
using SprintZero1.Factories;
using System.Collections.Generic;

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
            /*myGame = game;
            character = game.enemy;*/
        }

        public void Execute()
        {
            //
        }
    }
}

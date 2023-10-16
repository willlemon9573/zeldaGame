using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class NextEnemyCommand : ICommand
    {
        private readonly List<string> enemyNames;
        private readonly Game1 myGame;
        private readonly EnemyFactory myEnemyFactory;
        private readonly int totalEnemies;
        public NextEnemyCommand(Game1 game)
        {
            myGame = game;
            myEnemyFactory = EnemyFactory.Instance;
            enemyNames = myEnemyFactory.EnemyNamesList;
            totalEnemies = enemyNames.Count;
        }

        public void Execute()
        {
            /* myGame.OnScreenEnemyIndex = (myGame.OnScreenEnemyIndex + 1) % totalEnemies; // clock arithmetic [0, totalBlocks]
             myGame.enemy.EnemySprite = myEnemyFactory.CreateEnemySprite(enemyNames[myGame.OnScreenEnemyIndex], new Vector2(600, 300), 0);*/
        }
    }
}

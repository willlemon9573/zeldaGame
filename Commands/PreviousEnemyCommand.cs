using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class PreviousEnemyCommand : ICommand
    {
        private readonly List<string> enemyNames;
        private readonly Game1 myGame;
        private readonly IEnemyFactory myEnemyFactory;
        private readonly int totalEnemies;
        public PreviousEnemyCommand(Game1 game)
        {
            myGame = game;
            myEnemyFactory = EnemyFactory.Instance;
            enemyNames = myEnemyFactory.EnemyNamesList;
            totalEnemies = enemyNames.Count;
        }

        public void Execute()
        {
            /*myGame.OnScreenEnemyIndex = (myGame.OnScreenEnemyIndex - 1 + totalEnemies) % totalEnemies; // clock arithmetic [0, totalBlocks]
            myGame.enemy.EnemySprite = myEnemyFactory.CreateEnemySprite(enemyNames[myGame.OnScreenEnemyIndex], new Vector2(600, 300), 0); */
        }
    }
}

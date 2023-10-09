using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetPreviousTileCommand : ICommand
    {
        private readonly List<string> blockNames;
        private readonly Game1 myGame;
        private readonly ITileSpriteFactory myBlockFactory;
        private readonly int totalBlocks;
        public GetPreviousTileCommand(Game1 game)
        {
            myGame = game;
            /*myBlockFactory = TileSpriteFactory.Instance;*/
            /* blockNames = myBlockFactory.BlockNamesList;*/
            /*totalBlocks = blockNames.Count;*/
        }

        public void Execute()
        {
            // myGame.OnScreenBlockIndex = (myGame.OnScreenBlockIndex + 1) % totalBlocks; // clock arithmetic [0, totalBlocks]
            // myGame.NonMovingBlock = myBlockFactory.CreateNewTileSprite(blockNames[myGame.OnScreenBlockIndex], new Vector2(200, 230));
        }
    }
}

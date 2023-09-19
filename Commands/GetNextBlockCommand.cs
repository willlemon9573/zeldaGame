using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetNextBlockCommand : ICommand
    {
        private readonly List<string> blockNames;
        private readonly Game1 myGame;
        private readonly IBlockFactory myBlockFactory;
        private int totalBlocks;
        public GetNextBlockCommand(Game1 game)
        {
            myGame = game;
            myBlockFactory = BlockFactory.Instance;
            blockNames = myBlockFactory.BlockNamesList;
            totalBlocks = blockNames.Count;
        }

        public void Execute()
        {
            myGame.OnScreenBlockIndex = (myGame.OnScreenBlockIndex + 1) % totalBlocks;
            myGame.NonMovingBlock = myBlockFactory.CreateNonMovingBlockSprite(blockNames[myGame.OnScreenBlockIndex]);
        }
    }
}

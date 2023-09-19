using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetPreviousBlockCommand : ICommand
    {
        private readonly List<string> blockNames;
        private readonly Game1 myGame;
        private readonly IBlockFactory myBlockFactory;
        private readonly int totalBlocks;
        public GetPreviousBlockCommand(Game1 game)
        {
            myGame = game;
            myBlockFactory = BlockFactory.Instance;
            blockNames = myBlockFactory.BlockNamesList;
            totalBlocks = blockNames.Count;
        }
        public void Execute()
        {
            myGame.OnScreenBlockIndex = (myGame.OnScreenBlockIndex - 1 + totalBlocks) % totalBlocks;
            myGame.NonMovingBlock = myBlockFactory.CreateNonMovingBlockSprite(blockNames[myGame.OnScreenBlockIndex]);
        }
    }
}

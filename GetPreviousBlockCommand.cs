

using System.Diagnostics;

namespace SprintZero1
{
    public class GetPreviousBlockCommand : ICommand
    {
        private string[] blockNames;
        private Game1 myGame;
        private readonly IBlockFactory myBlockFactory;
        int totalBlocks;
        
        public GetPreviousBlockCommand(Game1 game)
        {
            myGame = game;
            blockNames = new string[] { "flat", "pyramid", "stairs", "greybrick" };
            myBlockFactory = BlockFactory.Instance;
            totalBlocks = 4;
        }
        public void Execute()
        {
            myGame.OnScreenBlockPos = ((myGame.OnScreenBlockPos - 1) + totalBlocks) % totalBlocks;
            Debug.WriteLine(blockNames[myGame.OnScreenBlockPos]);
            myGame.NonMovingBlock = myBlockFactory.CreateNonMovingBlockSprite(blockNames[myGame.OnScreenBlockPos]);
        }
    }
}

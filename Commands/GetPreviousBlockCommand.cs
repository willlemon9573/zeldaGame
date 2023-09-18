using SprintZero1.Factories;

namespace SprintZero1.Commands
{
    public class GetPreviousBlockCommand : ICommand
    {
        private readonly string[] blockNames;
        private readonly Game1 myGame;
        private readonly IBlockFactory myBlockFactory;
        private readonly int totalBlocks;

        public GetPreviousBlockCommand(Game1 game)
        {
            myGame = game;
            blockNames = new string[] { "flat", "pyramid", "stairs", "greybrick" };
            myBlockFactory = BlockFactory.Instance;
            totalBlocks = 4;
        }
        public void Execute()
        {
            myGame.OnScreenBlockIndex = (myGame.OnScreenBlockIndex - 1 + totalBlocks) % totalBlocks;
            myGame.NonMovingBlock = myBlockFactory.CreateNonMovingBlockSprite(blockNames[myGame.OnScreenBlockIndex]);
        }
    }
}

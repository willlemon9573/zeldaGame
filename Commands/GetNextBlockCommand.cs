using SprintZero1.Factories;

namespace SprintZero1.Commands
{
    public class GetNextBlockCommand : ICommand
    {
        private readonly string[] blockNames;
        private readonly Game1 myGame;
        private readonly IBlockFactory myBlockFactory;
        public GetNextBlockCommand(Game1 game)
        {
            myGame = game;
            blockNames = new string[] { "flat", "pyramid", "stairs", "greybrick" };
            myBlockFactory = BlockFactory.Instance;
        }

        public void Execute()
        {
            myGame.OnScreenBlockIndex = (myGame.OnScreenBlockIndex + 1) % 4;
            myGame.NonMovingBlock = myBlockFactory.CreateNonMovingBlockSprite(blockNames[myGame.OnScreenBlockIndex]);
        }
    }
}


namespace SprintZero1
{
    internal class HorizontalMovingAnimatedSoraCommand : ICommand
    {
        private readonly Game1 myGame;
        /// <summary>
        /// Construct a new object of the Horizontal Moving Animated Sora command
        /// </summary>
        /// <param name="myGame">Game object</param>
        public HorizontalMovingAnimatedSoraCommand(Game1 myGame)
        {
            this.myGame = myGame;
        }

        public void Execute()
        {
            myGame.SetSprite(new HorizontalMovingAnimatedSora());
        }
    }
}

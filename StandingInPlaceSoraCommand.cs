

namespace SprintZero1
{
    public class StandingInPlaceSoraCommand : ICommand
    {
        private readonly Game1 myGame;
        /// <summary>
        /// Construct Command to change the current sprite to 
        /// a Standing In Place Sora sprite
        /// </summary>
        /// <param name="game">The game object</param>
        public StandingInPlaceSoraCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.SetSprite(new StandingInPlaceSora());
        }
    }
}

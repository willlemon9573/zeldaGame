
namespace SprintZero1
{
    public class ExitCommand : ICommand
    {
        private readonly Game1 myGame;

        /// <summary>
        /// Construct a new Exit Command object
        /// </summary>
        /// <param name="game">The game object</param>
        public ExitCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.Exit(); // close the game
        }
    }
}

namespace SprintZero1.Commands.PlayerCommands
{
    public class ExitCommand : BaseChangeGameStateCommand
    {
        private Game1 _game;
        /// <summary>
        /// Command for exiting the game
        /// </summary>
        /// <param name="game">The current game instance</param>
        public ExitCommand(Game1 game) : base()
        {
            _game = game;
        }

        public override void Execute()
        {
            _game.Exit();
        }
    }
}

namespace SprintZero1.Commands.PlayerCommands
{
    /// <summary>
    /// Basis for all the commands the player can do to change the state of game
    /// </summary>
    public abstract class BaseChangeGameStateCommand : ICommand
    {
        /// <summary>
        /// Delegates for handling game state changes
        /// </summary>
        protected Game1 _game;

        /// <summary>
        /// Default implementation for creating game state change commands based on player input
        /// </summary>
        /// <param name="gameState">base game state class reference</param>
        public BaseChangeGameStateCommand(Game1 game)
        {
            _game = game;
        }

        public abstract void Execute();
    }
}

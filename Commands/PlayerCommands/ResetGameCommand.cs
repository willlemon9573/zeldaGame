using SprintZero1.Enums;
namespace SprintZero1.Commands.PlayerCommands
{
    internal class ResetGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for Resetting the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public ResetGameCommand(Game1 game) : base(game) { }

        /// <summary>
        /// Execute the command to handle resetting the game
        /// </summary>
        public override void Execute()
        {
            _game.GameState.ChangeGameState(GameState.Reset);
            _game.GameState.Handle();
        }
    }
}

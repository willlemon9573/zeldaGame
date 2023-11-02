using SprintZero1.Enums;

namespace SprintZero1.Commands.PlayerCommands
{

    public class UnpauseGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for unpausing the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public UnpauseGameCommand(Game1 game) : base(game)
        {
        }

        /// <summary>
        /// Execute the command for unpausing the game
        /// </summary>
        public override void Execute()
        {
            _game.GameState.ChangeGameState(GameState.ItemSelectionScreen);
            _game.GameState.Handle();
        }
    }
}

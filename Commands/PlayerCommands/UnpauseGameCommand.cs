using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{

    public class UnpauseGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for unpausing the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public UnpauseGameCommand(GameChangeStateHandler gameChangeStateHandler, GameStateHandler gameStateHandler) : base(gameChangeStateHandler, gameStateHandler)
        {
        }

        /// <summary>
        /// Execute the command for unpausing the game
        /// </summary>
        public override void Execute()
        {
            _gameChangeStateHandler.Invoke(GameState.Playing);
            _gameStateHandler.Invoke();
        }
    }
}

using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class PauseGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for pausing the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public PauseGameCommand(GameChangeStateHandler gameChangeStateHandler, GameStateHandler gameStateHandler) : base(gameChangeStateHandler, gameStateHandler)
        {
        }

        /// <summary>
        /// Execute the command to pause the game
        /// </summary>
        public override void Execute()
        {
            _gameChangeStateHandler.Invoke(GameState.Paused);
            _gameStateHandler.Invoke();
        }
    }
}

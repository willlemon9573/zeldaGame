using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class ResetGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for Resetting the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public ResetGameCommand(GameChangeStateHandler gameChangeStateHandler, GameStateHandler gameStateHandler) : base(gameChangeStateHandler, gameStateHandler)
        {
        }


        /// <summary>
        /// Execute the command to handle resetting the game
        /// </summary>
        public override void Execute()
        {
            _gameChangeStateHandler.Invoke(GameState.Reset);
            _gameStateHandler.Invoke();
        }
    }
}

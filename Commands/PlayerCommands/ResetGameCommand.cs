using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class ResetGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command used for resetting the game
        /// </summary>
        /// <param name="gameState">Base Game State reference</param>
        public ResetGameCommand(BaseGameState gameState) : base(gameState) { }

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

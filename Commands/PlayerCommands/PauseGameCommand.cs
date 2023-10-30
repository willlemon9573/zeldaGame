using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class PauseGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for pausing the game
        /// </summary>
        /// <param name="gameState">Base Game State reference</param>
        public PauseGameCommand(BaseGameState gameState) : base(gameState) { }
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

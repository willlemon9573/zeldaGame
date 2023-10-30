using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{

    public class UnpauseCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for unpausing the game
        /// </summary>
        /// <param name="gameState">Base Game State reference</param>
        public UnpauseCommand(BaseGameState gameState) : base(gameState) { }
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

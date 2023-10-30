using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class CloseInventoryCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for closing player inventory
        /// </summary>
        /// <param name="gameState">Base Game State reference</param>
        public CloseInventoryCommand(BaseGameState gameState) : base(gameState) { }
        /// <summary>
        /// Execute the command for closing player inventory
        /// </summary>
        public override void Execute()
        {
            _gameChangeStateHandler.Invoke(GameState.Playing);
            _gameStateHandler.Invoke();
        }
    }
}

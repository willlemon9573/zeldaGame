using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class OpenInventoryCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for opening the player inventory
        /// </summary>
        /// <param name="gameState">Base Game State reference</param>
        public OpenInventoryCommand(BaseGameState gameState) : base(gameState) { }
        /// <summary>
        /// Execute the command for opening the player inventory
        /// </summary>
        public override void Execute()
        {
            _gameChangeStateHandler.Invoke(GameState.ItemSelectionScreen);
            _gameStateHandler.Invoke();
        }
    }
}

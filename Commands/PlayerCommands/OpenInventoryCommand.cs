using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class OpenInventoryCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for opening player inventory
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public OpenInventoryCommand(GameChangeStateHandler gameChangeStateHandler, GameStateHandler gameStateHandler) : base(gameChangeStateHandler, gameStateHandler) { }

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

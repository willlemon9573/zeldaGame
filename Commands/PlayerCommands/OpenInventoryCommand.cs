using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class OpenInventoryCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for opening player inventory
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public OpenInventoryCommand(Game1 game) : base(game) { }

        /// <summary>
        /// Execute the command for opening the player inventory
        /// </summary>
        public override void Execute()
        {
            GameStatesManager.ChangeGameState(GameState.ItemSelectionScreen);
            GameStatesManager.CurrentState.Handle();
        }
    }
}

using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class OpenInventoryCommand : BaseChangeGameStateCommand
    {

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

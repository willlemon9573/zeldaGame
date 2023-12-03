using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class CloseInventoryCommand : BaseChangeGameStateCommand
    {

        /// <summary>
        /// Execute the command for closing player inventory
        /// </summary>
        public override void Execute()
        {
            GameStatesManager.ChangeGameState(GameState.Playing);
        }
    }
}

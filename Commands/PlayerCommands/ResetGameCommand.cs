using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class ResetGameCommand : BaseChangeGameStateCommand
    {

        /// <summary>
        /// Execute the command to handle resetting the game
        /// </summary>
        public override void Execute()
        {
            GameStatesManager.ChangeGameState(GameState.Reset);
            GameStatesManager.CurrentState.Handle();
        }
    }
}

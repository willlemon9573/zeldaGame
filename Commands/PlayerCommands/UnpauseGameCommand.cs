using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{

    public class UnpauseGameCommand : BaseChangeGameStateCommand
    {

        /// <summary>
        /// Execute the command for unpausing the game
        /// </summary>
        public override void Execute()
        {
            GameStatesManager.ChangeGameState(GameState.Playing);
            GameStatesManager.CurrentState.Handle();
        }
    }
}

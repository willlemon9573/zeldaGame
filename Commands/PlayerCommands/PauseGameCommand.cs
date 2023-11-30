using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class PauseGameCommand : BaseChangeGameStateCommand
    {

        public PauseGameCommand() : base()
        {
        }

        /// <summary>
        /// Execute the command to pause the game
        /// </summary>
        public override void Execute()
        {
            GameStatesManager.ChangeGameState(GameState.Paused);
            GameStatesManager.CurrentState.Handle();
        }
    }
}

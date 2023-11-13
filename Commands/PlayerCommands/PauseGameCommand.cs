using SprintZero1.Enums;
using SprintZero1.Managers;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class PauseGameCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for pausing the game
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public PauseGameCommand(Game1 game) : base(game)
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

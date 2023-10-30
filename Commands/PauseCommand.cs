using SprintZero1.Enums;
using SprintZero1.Managers;
namespace SprintZero1.Commands
{
    public class PauseCommand : ICommand
    {
        private readonly Game1 myGame;

        /// <summary>
        /// Construct a new Exit Command object
        /// </summary>
        /// <param name="game">The game object</param>
        public PauseCommand()
        {
        }

        public void Execute()
        {
            if (ProgramManager.GameState == GameState.Playing)
            {
                ProgramManager.GameState = GameState.Paused;
            }
            else if (ProgramManager.GameState == GameState.Paused)
            {
                ProgramManager.GameState = GameState.Playing;
            }
        }
    }
}

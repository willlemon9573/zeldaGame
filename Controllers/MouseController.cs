using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;

namespace SprintZero1.Controllers
{
    /// <summary>
    /// TODO: Need to update for Sprint 4 - Removal of unused methods
    /// </summary>
    internal class MouseController : IController
    {
        private MouseState oldState;
        private GetPreviousLevelCommand getPreviousLevelCommand;
        private GetNextLevelCommand getNextLevelCommand;
        public MouseController(Game1 myGame)
        {
            getNextLevelCommand = new GetNextLevelCommand();
            getPreviousLevelCommand = new GetPreviousLevelCommand();
        }

        /// <summary>
        /// Calls the specific command located at {@code commands[i]} and executes 
        /// said command based on which quadrant the mouse position is located 
        /// if the left mouse button is clicked and released
        /// </summary>
        /// <param name="mouseLocation">XY coordinates of the current mouse position</param>
        private void ExecuteLeftMouseCommands()
        {
            getPreviousLevelCommand.Execute();
        }

        /// <summary>
        /// Executes the command based on whether the right mouse button is clicked or not
        /// </summary>
        private void ExecuteRightMouseCommand()
        {
            getNextLevelCommand.Execute();
        }

        public void LoadDefaultCommands(Game1 game, ICombatEntity playerEntity)
        {
            // Unimplemented for this - will update for Sprint 4
        }

        public void Update()
        {
            MouseState newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                ExecuteLeftMouseCommands();
            }
            else if (newState.RightButton == ButtonState.Pressed && oldState.RightButton == ButtonState.Released)
            {
                ExecuteRightMouseCommand();
            }
            oldState = newState;
        }


    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using System.Drawing;

namespace SprintZero1.Controllers
{
    /// <summary>
    /// TODO: Need to update for Sprint 4 - Removal of unused methods
    /// </summary>
    internal class MouseController : IController
    {
        private MouseState _oldMouseState;
        private GetPreviousLevelCommand _getPreviousRoom;
        private GetNextLevelCommand _getNextRoom;
        private readonly Game1 _game;
        private readonly Rectangle _windowRegion;


        /// <summary>
        /// Check if the mouse is current in the window or not
        /// </summary>
        /// <param name="mouseState">The current state of the mouse</param>
        /// <returns>true if the mouse is in the window, false otherwise</returns>
        private bool IsInWindow(MouseState mouseState)
        {
            int x = mouseState.X;
            int y = mouseState.Y;
            return _windowRegion.Contains(x, y);
        }
        /// <summary>
        /// Check if the left or right button is currently pressed
        /// </summary>
        /// <param name="mouseButton">The left or right mouse button</param>
        /// <returns>true if the button is down, false othersise</returns>
        private bool IsButtonDown(ButtonState mouseButton)
        {
            return mouseButton == ButtonState.Pressed;
        }

        public MouseController(Game1 myGame)
        {
            _getNextRoom = new GetNextLevelCommand();
            _getPreviousRoom = new GetPreviousLevelCommand();
            _game = myGame;
            /* setup window region rectangle for checking if the mouse is in the window or not */
            int _gameHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int _gameWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            const int x = 0, y = 0;
            _windowRegion = new Rectangle(x, y, _gameWidth, _gameHeight);
        }

        /// <summary>
        /// Calls the specific command located at {@code commands[i]} and executes 
        /// said command based on which quadrant the mouse position is located 
        /// if the left mouse button is clicked and released
        /// </summary>
        /// <param name="mouseLocation">XY coordinates of the current mouse position</param>
        private void ExecuteLeftMouseCommands()
        {
            _getPreviousRoom.Execute();
        }

        /// <summary>
        /// Executes the command based on whether the right mouse button is clicked or not
        /// </summary>
        private void ExecuteRightMouseCommand()
        {
            _getNextRoom.Execute();
        }

        public void LoadControls()
        {

        }

        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();
            /* if the game is not active just return */
            if (_game.IsActive == false) { return; }
            /*
             * Execute command only if a player's mouse is in the window and only after they click and let go of the mouse button.
             */
            if (IsInWindow(currentMouseState) && IsButtonDown(_oldMouseState.LeftButton) && !IsButtonDown(currentMouseState.LeftButton))
            {
                ExecuteLeftMouseCommands();
            }
            else if (IsInWindow(currentMouseState) && IsButtonDown(_oldMouseState.RightButton) && !IsButtonDown(currentMouseState.RightButton))
            {
                ExecuteRightMouseCommand();
            }

            _oldMouseState = currentMouseState;
        }


    }
}

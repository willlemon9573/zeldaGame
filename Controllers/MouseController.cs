using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace SprintZero1.Controllers
{
    internal class MouseController : IController
    {
        private readonly ICommand[] commands;
        private readonly Rectangle[] quadrants;
        private MouseState oldState;
        private LevelManager levelManager;
        private GetPreviousLevelCommand getPreviousLevelCommand;
        private GetNextLevelCommand getNextLevelCommand;
        public MouseController(Game1 myGame)
        {
            levelManager = new LevelManager(myGame);
            getNextLevelCommand = new GetNextLevelCommand(myGame);
            getPreviousLevelCommand = new GetPreviousLevelCommand(myGame);
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
        /* Can remove this */
        /// <summary>
        /// Add the values relative to each quadrant into the quadrants array
        /// </summary>
        private void CreateQuadrantArray()
        {
            int[,] quadrant_values = new int[,] { { 11, 32, 396, 251 }, { 413, 31, 397, 253 },
                { 8, 290, 398, 217 }, { 412, 291, 396, 222 } };
            for (int i = 0; i < quadrants.Length; i++)
            {
                quadrants[i] = new Rectangle(quadrant_values[i, 0], quadrant_values[i, 1], quadrant_values[i, 2], quadrant_values[i, 3]);
            }

        }

        /// <summary>
        /// Construct a new object for the Mouse Controller
        /// </summary>
        public MouseController()
        {
            commands = new ICommand[5];
            oldState = Mouse.GetState();
            quadrants = new Rectangle[4];
            CreateQuadrantArray();
        }

        public void LoadDefaultCommands(Game1 game, IEntity playerEntity)
        {
            
        }

        public void Update()
        {
            MouseState newState = Mouse.GetState();
            if (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
            {
                ExecuteLeftMouseCommands();
            }
            else if (newState.RightButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                ExecuteRightMouseCommand();
            }
            oldState = newState;
        }

        public void AddCommand(Keys key, ICommand value)
        {
            throw new System.NotImplementedException();
        }
    }
}

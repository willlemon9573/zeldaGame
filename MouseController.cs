﻿

using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace SprintZero1
{
    public class MouseController : IController
    {
        private readonly ICommand[] commands;
        private readonly Rectangle[] quadrants;
        private MouseState oldState;
        
        /// <summary>
        /// Calls the specific command located at {@code commands[i]} and executes 
        /// said command based on which quadrant the mouse position is located 
        /// if the left mouse button is clicked and released
        /// </summary>
        /// <param name="mouseLocation">XY coordinates of the current mouse position</param>
        private void ExecuteLeftMouseCommands(Vector2 mouseLocation)
        {
            Point mousePoint = new Point((int)mouseLocation.X, (int)mouseLocation.Y);
            int i = 1;
            foreach (Rectangle quadrant in quadrants)
            {
                if (quadrant.Contains(mousePoint))
                {
                    commands[i].Execute();
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// Executes the command based on whether the right mouse button is clicked or not
        /// </summary>
        private void ExecuteRightMouseCommand()
        {
            commands[0].Execute();
        }

        /// <summary>
        /// Add the values relative to each quadrant into the quadrants array
        /// </summary>
        private void CreateQuadrantArray()
        {
            int[,] quadrant_values = new int[,] { { 11, 32, 396, 251 }, { 413, 31, 397, 253 },
                { 8, 290, 398, 217 }, { 412, 291, 396, 222 } };
            for (int i = 0; i < quadrants.Length; i++) {
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

        public void LoadDefaultCommands(Game1 game)
        {
           
        }

        public void Update()
        {
            MouseState newState = Mouse.GetState();
            if (oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed) {
                ExecuteLeftMouseCommands(new Vector2(newState.X, newState.Y));
            } else if (newState.RightButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) {
                ExecuteRightMouseCommand();
            }
            oldState = newState;
        }
    }
}

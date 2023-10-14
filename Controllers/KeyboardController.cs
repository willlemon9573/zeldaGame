using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> keyboardMap;
        List<Keys> movementKeys = new List<Keys>() { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.W, Keys.S, Keys.A, Keys.D };
        List<Keys> previouslyPressedKeys = new List<Keys>();
        Stack<Keys> priorityKeyStack = new Stack<Keys>();
        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            keyboardMap = new Dictionary<Keys, ICommand>();
        }
        /// <summary>
        /// Removes any key not being pressed from the priority key stack
        /// </summary>
        private void FlipAndClean(KeyboardState currentKeyboardState)
        {
            if (priorityKeyStack.Count > 0)
            {
                Keys k = priorityKeyStack.Pop();
                FlipAndClean(currentKeyboardState);
                if (Keyboard.GetState().IsKeyDown(k))
                {
                    priorityKeyStack.Push(k);
                }
            }
        }

        /// <summary>
        /// A function to allow multiple movement keys to be pressed at once
        /// </summary>
        /// <param name="movementKey">the current movement key that needs to be checked</param>
        /// <param name="currentKeyboardState">the current stay of the keyboard</param>
        void HandleMovementKey(Keys movementKey, KeyboardState currentKeyboardState)
        {
            /* Add key to stack if it's not in the stack already */
            if (!priorityKeyStack.Contains(movementKey))
            {
                priorityKeyStack.Push(movementKey);
                /* don't execute key because it will push through colliders 
                 * if the key is pressed more than once */
                return;
            }
            /* Key is in stack already, check if this key is the movement command that the
             * player wants to execute */
            while (priorityKeyStack.Count > 0)
            {
                Keys priorityKey = priorityKeyStack.Pop();
                /* execute priority key only when the key being checked matches. */
                if (priorityKey == movementKey)
                {
                    priorityKeyStack.Push(priorityKey);
                    keyboardMap[priorityKey].Execute();
                    break;
                }
                else if (currentKeyboardState.IsKeyDown(priorityKey))
                {
                    /* clean up priorty key stack as the current priority key was let go
                     * then push the priority key back onto the stack */
                    FlipAndClean(currentKeyboardState);
                    priorityKeyStack.Push(priorityKey);
                    break;
                }
            }

        }

        public void LoadDefaultCommands(Game1 game, IEntity playerEntity)
        {
            /* directional commands */
            keyboardMap.Add(Keys.Up, new MoveUpCommand(playerEntity));
            keyboardMap.Add(Keys.Down, new MoveDownCommand(playerEntity));
            keyboardMap.Add(Keys.Left, new MoveLeftCommand(playerEntity));
            keyboardMap.Add(Keys.Right, new MoveRightCommand(playerEntity));
            keyboardMap.Add(Keys.W, new MoveUpCommand(playerEntity));
            keyboardMap.Add(Keys.S, new MoveDownCommand(playerEntity));
            keyboardMap.Add(Keys.A, new MoveLeftCommand(playerEntity));
            keyboardMap.Add(Keys.D, new MoveRightCommand(playerEntity));
            /* Attack Commands */
            keyboardMap.Add(Keys.Z, new LinkAttackCommand(playerEntity));
            /* Other commands */
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            keyboardMap.Add(Keys.U, new PreviousItemCommand(game));
            keyboardMap.Add(Keys.I, new NextItemCommand(game));
            keyboardMap.Add(Keys.O, new PreviousEnemyCommand(game));
            keyboardMap.Add(Keys.P, new NextEnemyCommand(game));
            keyboardMap.Add(Keys.Y, new GetPreviousTileCommand(game));
            keyboardMap.Add(Keys.T, new GetNextTileCommands(game));
        }

        public void Update()
        {
            /* handling movement? */
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                bool isMoveKey = movementKeys.Contains(key);
                bool isCommandKey = keyboardMap.ContainsKey(key);
                bool hasBeenPressed = previouslyPressedKeys.Contains(key);
                if (isMoveKey)
                {
                    HandleMovementKey(key, currentKeyboardState);
                }
                else if (isCommandKey && !hasBeenPressed)
                {
                    keyboardMap[key].Execute();
                }

            }
            /* clean up priority key stack if no buttons are pressed */
            if (pressedKeys.Length == 0 && priorityKeyStack.Count > 0)
            {
                priorityKeyStack.Clear();
            }

            previouslyPressedKeys = pressedKeys.ToList<Keys>();
        }
    }
}
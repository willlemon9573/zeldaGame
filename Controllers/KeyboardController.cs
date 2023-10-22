using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.Controllers
{
    internal class KeyboardController : IController
    {
        private ICommand _idleCommand;
        private readonly Dictionary<Keys, ICommand> keyboardMap;
        private List<Keys> _movementKeyList;
        private List<Keys> _previouslyPressedKeys;
        private Stack<Keys> movementKeyStack;
        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            keyboardMap = new Dictionary<Keys, ICommand>();
            _previouslyPressedKeys = new List<Keys>();
            movementKeyStack = new Stack<Keys>();
            _movementKeyList = new List<Keys>() { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.W, Keys.S, Keys.A, Keys.D };
        }

        /// <summary>
        /// Recursively removes movement keys from the movement key stack that are not in the list of pressed keys
        /// </summary>
        /// <param name="pressedKeys">collection of currently pressed keys</param>
        private void FlipAndClean(Keys[] pressedKeys)
        {
            if (movementKeyStack.Count > 0)
            {
                Keys k = movementKeyStack.Pop();
                FlipAndClean(pressedKeys);
                if (pressedKeys.Contains(k))
                {
                    movementKeyStack.Push(k);
                }
            }
        }

        /// <summary>
        /// A function to allow multiple movement keys to be pressed at once while maintaining the most recent key command is executed
        /// </summary>
        /// <param name="movementKey">the current movement key that needs to be checked</param>
        void HandleMovementKey(Keys movementKey)
        {
            /* 
             * Add key to stack if it's not in the stack already and execute that key
             * else execute the command of the movement key at the top of the stack
             */
            if (!movementKeyStack.Contains(movementKey))
            {
                movementKeyStack.Push(movementKey);
            }
            else if (movementKey == movementKeyStack.Peek())
            {
                Keys keyRef = movementKeyStack.Peek();
                keyboardMap[keyRef].Execute();
            }

        }
        public void LoadDefaultCommands(Game1 game, ICombatEntity playerEntity)
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
            keyboardMap.Add(Keys.Z, new SwordAttackCommand(playerEntity));
            /* Other commands */
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            _idleCommand = new ReturnPlayerToIdleCommand(playerEntity);
        }

        public void Update()
        {
            /* handling movement? */
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            int movementKeyCount = 0;

            /* iterate over pressed key collection executing only valid keys in the keyboard map 
               keeping track of the amount of movement keys that are also currently being pressed
            */
            foreach (Keys key in pressedKeys)
            {
                if (_movementKeyList.Contains(key))
                {
                    HandleMovementKey(key);
                    movementKeyCount++;
                    
                }
                else if (keyboardMap.ContainsKey(key) && !_previouslyPressedKeys.Contains(key))
                {
                    keyboardMap[key].Execute();
                }
            }

            /* Clean up movement key stack and/or set player to idle when 
             * no keys are pressed. Checking both movement key count and previously pressed
             * keys to prevent _idleCommand from executing more than once
             */
            if (movementKeyCount == 0 && _previouslyPressedKeys.Count > 0) 
            {
                _idleCommand.Execute();
                FlipAndClean(pressedKeys);
            }
            else if (movementKeyCount < movementKeyStack.Count)
            {
                FlipAndClean(pressedKeys);
            }

            /* store pressed keys as previously pressed keys */
            _previouslyPressedKeys = pressedKeys.ToList<Keys>();
        }
    }
}
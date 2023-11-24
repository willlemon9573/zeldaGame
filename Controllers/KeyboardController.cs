using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SprintZero1.Controllers
{
    public delegate void PausedStateUpdater(Game1 game);
    internal class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _keyboardMap;
        private readonly List<Keys> _movementKeyList;
        private List<Keys> _previouslyPressedKeys;
        private readonly Stack<Keys> _movementKeyStack;
        private ICommand _playerIdleCommand;
        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            _previouslyPressedKeys = new List<Keys>();
            _movementKeyStack = new Stack<Keys>();
            _movementKeyList = new List<Keys>() { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.W, Keys.S, Keys.A, Keys.D };
        }

        /// <summary>
        /// Recursively removes movement keys from the movement key stack that are not in the list of pressed keys
        /// </summary>
        /// <param name="pressedKeys">collection of currently pressed keys</param>
        private void FlipAndClean(Keys[] pressedKeys)
        {
            if (_movementKeyStack.Count > 0)
            {
                Keys k = _movementKeyStack.Pop();
                FlipAndClean(pressedKeys);
                if (pressedKeys.Contains(k))
                {
                    _movementKeyStack.Push(k);
                }
            }
        }

        /// <summary>
        /// A function to allow multiple movement keys to be pressed at once while maintaining the most recent key command is executed
        /// </summary>
        /// <param name="movementKey">the current movement key that needs to be checked</param>
        private void HandleMovementKey(Keys movementKey)
        {
            /* 
             * Add key to stack if it's not in the stack already and execute that key
             * else execute the command of the movement key at the top of the stack
             */
            if (!_movementKeyStack.Contains(movementKey))
            {
                _movementKeyStack.Push(movementKey);
            }
            else if (movementKey == _movementKeyStack.Peek())
            {
                Keys keyRef = _movementKeyStack.Peek();
                _keyboardMap[keyRef].Execute();
            }
        }

        /// <summary>
        /// Load the controls for the specific player.
        /// </summary>
        /// <param name="player">The player the controls will be loaded for</param>
        public void LoadControls(IEntity player)
        {
            _keyboardMap = ControlsManager.GetKeyboardControls(player);
            _playerIdleCommand = new PlayerIdleCommand(player as PlayerEntity);
        }

        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            // no need to update if no keys have been pressed
            if (pressedKeys.Length == 0 && _movementKeyStack.Count == 0 && _previouslyPressedKeys.Count == 0) { return; }
            int totalKeyCount = 0;
            /* iterate over pressed key collection executing only valid keys in the keyboard map 
               keeping track of the amount of movement keys that are also currently being pressed
            */
            foreach (Keys key in pressedKeys)
            {
                if (_movementKeyList.Contains(key))
                {
                    HandleMovementKey(key);
                    totalKeyCount++;
                }
                else if (_keyboardMap.ContainsKey(key) && !_previouslyPressedKeys.Contains(key))
                {
                    _keyboardMap[key].Execute();
                }
            }

            /* Clean up movement key stack and/or set player to idle when 
             * no keys are pressed. Checking both movement key count and previously pressed
             * keys to prevent _idleCommand from executing more than once
             */
            if (totalKeyCount < _movementKeyStack.Count)
            {
                FlipAndClean(pressedKeys);
            }

            /* store pressed keys as previously pressed keys */
            _previouslyPressedKeys = pressedKeys.ToList<Keys>();
            /* Change player to idle state when there aren't any keys pressed */
            if (_previouslyPressedKeys.Count == 0 && pressedKeys.Length == 0)
            {
                _playerIdleCommand.Execute();
            }
        }
    }
}
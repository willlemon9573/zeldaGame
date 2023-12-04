using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Entities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Controllers
{
    internal class GamepadController : IController
    {
        private Dictionary<Buttons, ICommand> _gamepadMap;
        private readonly List<Buttons> _movementButtonList;
        private List<Buttons> _previousPressedButtons;
        private readonly Stack<Buttons> _movementButtonStack;
        private ICommand _playerIdleCommand;
        private readonly int _gamepadIndex;


        /// <summary>
        /// Constructor an object to control the keyboard
        /// </summary>
        public GamepadController(int index)
        {
            _gamepadMap = new Dictionary<Buttons, ICommand>();
            _previousPressedButtons = new List<Buttons>();
            _movementButtonStack = new Stack<Buttons>();
            _movementButtonList = new List<Buttons>() { Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight, Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight };
            _gamepadIndex = index;
        }

        /// <summary>
        /// Recursively removes movement buttons from the movement button 
        /// </summary>
        /// <param name="pressedButtons">collection of currently pressed buttons</param>
        private void FlipAndClean(List<Buttons> pressedButtons)
        {
            if (_movementButtonStack.Count > 0)
            {
                Buttons b = _movementButtonStack.Pop();
                FlipAndClean(pressedButtons);
                if (pressedButtons.Contains(b))
                {
                    _movementButtonStack.Push(b);
                }
            }
        }

        /// <summary>
        /// Function to allow multiple movement buttons
        /// </summary>
        /// <param name="movementButton">the current movement button</param>
        private void HandleMovementButtons(Buttons movementButton)
        {
            /*
             * Add button to stack if its not already in the stack
             * else execute the command
             */
            if (!_movementButtonStack.Contains(movementButton))
            {
                _movementButtonStack.Push(movementButton);
            }
            else if (movementButton == _movementButtonStack.Peek())
            {
                Buttons buttonRef = _movementButtonStack.Peek();
                _gamepadMap[buttonRef].Execute();
            }
        }

        public void LoadControls(IEntity player)
        {
            _gamepadMap = ControlsManager.GetGamePadControls(player);
            _playerIdleCommand = new PlayerIdleCommand(player as PlayerEntity);
        }


        /// <summary>
        /// Get a list of the currently pressed buttons
        /// </summary>
        /// <returns></returns>
        private List<Buttons> GetPressedButtons()
        {
            List<Buttons> pressedButtons = new List<Buttons>();

            foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
            {
                if (GamePad.GetState(_gamepadIndex).IsButtonDown(btn))
                {
                    pressedButtons.Add(btn);
                }
            }

            return pressedButtons;
        }

        public void Update()
        {
            List<Buttons> pressedButtons = GetPressedButtons();
            int totalButtonCount = 0;
            if (pressedButtons.Count == 0 && _movementButtonStack.Count == 0 && _previousPressedButtons.Count == 0) { return; }
            /* iterate over pressed button collection executing only valid keys in the gamepad map 
               keeping track of the amount of movement button that are also currently being pressed
            */
            foreach (Buttons button in pressedButtons)
            {
                if (_movementButtonList.Contains(button))
                {
                    HandleMovementButtons(button);
                }
                else if (_gamepadMap.ContainsKey(button) && !_previousPressedButtons.Contains(button))
                {
                    _gamepadMap[button].Execute();
                }
            }

            // clean movement stack when a button isn't pressed
            if (totalButtonCount < _movementButtonStack.Count)
            {
                FlipAndClean(pressedButtons);
            }

            _previousPressedButtons = pressedButtons.ToList<Buttons>();
            // set player to idle if no buttons have been pressed
            if (_previousPressedButtons.Count == 0)
            {
                _playerIdleCommand.Execute();
            }


        }


    }
}

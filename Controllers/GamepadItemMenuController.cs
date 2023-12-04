using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.MenuCommandsFolder;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.GameStateMenu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Controllers
{
    internal class GamepadItemMenuController : IController
    {
        private readonly List<Buttons> _buttonList;
        private Dictionary<Buttons, ICommand> _gamepadMap;
        private List<Buttons> _previouslyPressedButtons;
        private readonly int _gamepadIndex;
        private readonly ItemSelectionMenu _itemSelectionMenu;
        /// <summary>
        /// Constructs a new item menu controller controlled by a gamepad
        /// </summary>
        /// <param name="itemSelectionMenu">The item selection menu that is modified by the controller</param>
        /// <param name="gamepadIndex">The controller index</param>
        public GamepadItemMenuController(ItemSelectionMenu itemSelectionMenu, int gamepadIndex)
        {
            _itemSelectionMenu = itemSelectionMenu;
            _gamepadIndex = gamepadIndex;
            _buttonList = Enum.GetValues<Buttons>().ToList();
            _previouslyPressedButtons = new List<Buttons>();
        }

        public void LoadControls(IEntity playerEntity)
        {
            _gamepadMap = new Dictionary<Buttons, ICommand>()
            {
                {Buttons.DPadLeft, new GetPreviousWeaponCommand(_itemSelectionMenu) },
                {Buttons.DPadRight, new GetNextWeaponCommand(_itemSelectionMenu) },
                {Buttons.X, new SetCurrentWeaponToPlayerCommand(playerEntity, _itemSelectionMenu) },
                {Buttons.B, new UnpauseGameCommand() }
            };
        }

        /// <summary>
        /// Get a list of the currently pressed buttons
        /// </summary>
        /// <returns></returns>
        private List<Buttons> GetPressedButtons(GamePadState gamepadState)
        {
            // Selecting each button that has been pressed down and returning it as a list
            return _buttonList.Select(button => _buttonList[_buttonList.IndexOf(button)])
                .Where(button => gamepadState.IsButtonDown(button)).ToList();
        }

        public void Update()
        {
            List<Buttons> pressedButtons = GetPressedButtons(GamePad.GetState(_gamepadIndex));

            foreach (Buttons button in pressedButtons)
            {
                if (!_previouslyPressedButtons.Contains(button) && _gamepadMap.TryGetValue(button, out ICommand command))
                {
                    command.Execute();
                }
            }

            _previouslyPressedButtons = pressedButtons;
        }
    }
}

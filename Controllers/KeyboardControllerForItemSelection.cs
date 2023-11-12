using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands.MenuCommandsFolder;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using SprintZero1.GameStateMenu;

namespace SprintZero1.Controllers
{
    /// <summary>
    /// The KeyboardControllerForItemSelection class is responsible for handling keyboard inputs
    /// specifically for item selection in the game. It maps specific keys to corresponding commands
    /// and executes these commands based on player input.
    /// </summary>
    /// <author>Aren, Zihe Wang</author>
    internal class KeyboardControllerForItemSelection : IController
    {
        // Maps keyboard keys to ICommand objects
        private Dictionary<Keys, ICommand> _keyboardMap;

        // List of keys used for item selection
        private readonly List<Keys> _ItemSelectionKeyList;

        // List to track keys that were pressed in the previous frame
        private List<Keys> _previouslyPressedKeys;

        /*------------------------------------------------------------------------------------------------------------------*/
        //only for testing
        private Game1 _game;
        private IEntity _player;
        private ItemSelectionMenu _itemSelectionMenu;


        /// <summary>
        /// Constructor for KeyboardControllerForItemSelection. Initializes key lists.
        /// </summary>
        public KeyboardControllerForItemSelection(Game1 game, IEntity player, ItemSelectionMenu itemSelectionMenu)
        {
            _game = game;
            _player = player;
            _itemSelectionMenu = itemSelectionMenu;
            _previouslyPressedKeys = new List<Keys>();
            _ItemSelectionKeyList = new List<Keys>() { Keys.Left, Keys.Right, Keys.Z, Keys.Escape };
        }

        /// <summary>
        /// Loads the control mappings for a specific player.
        /// </summary>
        /// <param name="player">The player entity for which controls are being set up.</param>
        /// <param name="itemSelectionMenu">The item selection menu used in control mapping.</param>
        public void LoadControls(IEntity player)
        {
            // Instantiate command objects for each key action
            var getPreviousWeaponCommand = new GetPreviousWeaponCommand(_itemSelectionMenu);
            var getNextWeaponCommand = new GetNextWeaponCommand(_itemSelectionMenu);
            var unpauseGameCommand = new UnpauseGameCommand(_game);
            var setCurrentWeaponToPlayerCommand = new SetCurrentWeaponToPlayerCommand(_player, _itemSelectionMenu);

            // Map keys to their respective commands
            _keyboardMap[Keys.Left] = getPreviousWeaponCommand;
            _keyboardMap[Keys.Right] = getNextWeaponCommand;
            _keyboardMap[Keys.Z] = setCurrentWeaponToPlayerCommand;
            _keyboardMap[Keys.Escape] = unpauseGameCommand;
        }

        /// <summary>
        /// Updates the state of the keyboard controller, executing commands based on key presses.
        /// </summary>
        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();

            // Return early if no keys are pressed and no keys were previously pressed
            if (pressedKeys.Length == 0 && _previouslyPressedKeys.Count == 0)
            {
                return;
            }

            // Execute commands for newly pressed keys
            foreach (Keys key in pressedKeys)
            {
                if (_keyboardMap.ContainsKey(key) && !_previouslyPressedKeys.Contains(key))
                {
                    _keyboardMap[key].Execute();
                }
            }

            // Update previously pressed keys
            _previouslyPressedKeys = pressedKeys.ToList<Keys>();
        }
    }
}

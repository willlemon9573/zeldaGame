using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.MenuCommandsFolder;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.GameStateMenu;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Controllers
{
    /// <summary>
    /// The KeyboardControllerForItemSelection class is responsible for handling keyboard inputs
    /// specifically for item selection in the game. It maps specific keys to corresponding commands
    /// and executes these commands based on player input.
    /// </summary>
    /// <author>Aren, Zihe Wang</author>
    internal class KeyboardItemMenuController : IController
    {
        // Maps keyboard keys to ICommand objects
        private readonly Dictionary<Keys, ICommand> _keyboardMap;

        // List to track keys that were pressed in the previous frame
        private List<Keys> _previouslyPressedKeys;

        /*------------------------------------------------------------------------------------------------------------------*/
        //only for testing
        private readonly ItemSelectionMenu _itemSelectionMenu;


        /// <summary>
        /// Constructor for KeyboardControllerForItemSelection. Initializes key lists.
        /// </summary>
        public KeyboardItemMenuController(ItemSelectionMenu itemSelectionMenu)
        {
            _itemSelectionMenu = itemSelectionMenu;
            _previouslyPressedKeys = new List<Keys>() { Keys.I };
            _keyboardMap = new Dictionary<Keys, ICommand>();
        }

        /// <summary>
        /// Loads the control mappings for a specific player.
        /// </summary>
        /// <param name="player">The player entity for which controls are being set up.</param>
        public void LoadControls(IEntity player)
        {
            // Map keys to their respective commands
            _keyboardMap[Keys.Left] = new GetPreviousWeaponCommand(_itemSelectionMenu);
            _keyboardMap[Keys.Right] = new GetNextWeaponCommand(_itemSelectionMenu);
            _keyboardMap[Keys.B] = new SetCurrentWeaponToPlayerCommand(player, _itemSelectionMenu);
            _keyboardMap[Keys.I] = new UnpauseGameCommand();
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

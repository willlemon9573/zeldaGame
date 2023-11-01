using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.StatePatterns.GameStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using SprintZero1.XMLParsers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal static class ControlsManager
    {

        private const string ROOT_NAME = "Controllers";
        private const string KEYBOARD_ELEMENT = "Keyboard";
        private const string GAMEPAD_ELEMENT = "GamePad";

        private static readonly Dictionary<IEntity, Dictionary<Keys, ICommand>> _playerKeyboardControlsMap = new Dictionary<IEntity, Dictionary<Keys, ICommand>>();
        private static readonly Dictionary<IEntity, Dictionary<Buttons, ICommand>> _playerGamePadControlsMap = new Dictionary<IEntity, Dictionary<Buttons, ICommand>>();


        /// <summary>
        /// Creates the keyboard controls map for the player to access if they are using keyboard controls
        /// </summary>
        /// <param name="controllerSettingsPath">The path to the xml file that contains the settings the player will use</param>
        /// <param name="player">The player using the keyboard</param>
        /// <param name="gameState">The current state of the game</param>
        public static void CreateKeyboardControlsMap(string controllerSettingsPath, IEntity player, IGameState gameState)
        {
            XDocument controllerDocument = XDocument.Load(controllerSettingsPath);
            PlayerControlsParser controllerParser = new PlayerControlsParser(controllerDocument, ROOT_NAME);
            _playerKeyboardControlsMap.Add(player, controllerParser.ParseKeyboardControls(KEYBOARD_ELEMENT, (ICombatEntity)player, (BaseGameState)gameState));
        }

        /// <summary>
        /// Creates the gamepad controlls map for the player to access if they are using gamepad controls
        /// </summary>
        /// <param name="controllerSettingsPath">The path to the xml file that contains the settings the player will use</param>
        /// <param name="player">The player who uses the controls</param>
        /// <param name="gameState">The current state of the game</param>
        public static void CreateGamePadControlsMap(string controllerSettingsPath, IEntity player, IGameState gameState)
        {
            XDocument controllerDocument = XDocument.Load(controllerSettingsPath);
            PlayerControlsParser controlsParser = new PlayerControlsParser(controllerDocument, ROOT_NAME);
            _playerGamePadControlsMap.Add(player, controlsParser.ParseGamePadControls(GAMEPAD_ELEMENT, (ICombatEntity)player, (BaseGameState)gameState));
        }

        /// <summary>
        /// Gets the command map for keyboard controls
        /// </summary>
        /// <returns>the dictionary with the controls for the player using the keyboard</returns>
        public static Dictionary<Keys, ICommand> GetKeyboardControls(IEntity player)
        {
            return _playerKeyboardControlsMap[player];
        }
        /// <summary>
        /// Gets the player's controls map for gamepad controls
        /// </summary>
        /// <param name="player">The player the uses these controls</param>
        /// <returns>The dictionary with the controls for the player using a gamepad</returns>
        public static Dictionary<Buttons, ICommand> GetGamePadControls(IEntity player)
        {
            return _playerGamePadControlsMap[player];
        }



    }
}

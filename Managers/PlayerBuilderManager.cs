using SprintZero1.Controllers;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.XMLParsers;
using System;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal class PlayerBuilderManager : IPlayerBuilder
    {
        private IEntity _player; // 102, 100
        private readonly XDocument _characterDocument;
        private readonly CharacterParser _characterParser;
        private int _gamepadIndex;

        /// <summary>
        /// Constructor for the player builder instance. Creates a new XDocument so be sure to close
        /// when finished.
        /// </summary>
        /// <param name="characterXMLPath"></param>
        public PlayerBuilderManager(string characterXMLPath)
        {
            _characterDocument = XDocument.Load(characterXMLPath);
            _characterParser = new CharacterParser();
            _gamepadIndex = 0; // each gamepad has an index for individual characters starting at 0 up to the limit of controllers
        }

        public Tuple<IEntity, IController> BuildPlayerWithGamePad(string controllerSettingsPath, Game1 game, string characterName)
        {
            IEntity player = _characterParser.ParsePlayerCharacter(_characterDocument, characterName);
            ControlsManager.CreateGamePadControlsMap(controllerSettingsPath, player, game);
            IController gamepadController = new GamepadController(_gamepadIndex);
            gamepadController.LoadControls(player);
            _gamepadIndex++; // increment the gamepad index in case another player is made using a gamepad controller
            return Tuple.Create(player, gamepadController);
        }

        public Tuple<IEntity, IController> BuildPlayerWithKeyboard(string controllerSettingsPath, Game1 game, string characterName)
        {
            IEntity player = _characterParser.ParsePlayerCharacter(_characterDocument, characterName);
            ControlsManager.CreateKeyboardControlsMap(controllerSettingsPath, player, game);
            IController keyboardController = new KeyboardController();
            keyboardController.LoadControls(player);
            return Tuple.Create(player, keyboardController);
        }
    }
}

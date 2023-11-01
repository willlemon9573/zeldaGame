using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    /// <summary>
    /// Helper object for parsing player controls
    /// @Author Aaron Heishman
    /// </summary>
    internal class PlayerControlsParser
    {
        /* ----------------------------- Attribute/Element strings ----------------------------- */
        private readonly string KEYBOARD_KEY_ELEMENT = "key";
        private readonly string KEYBOARD_KEYS_ATTRIBUTE = "keys";
        private readonly string ACTION_ATTRIBUTE = "action";
        private const string GAMEPAD_BUTTONS_ATTRIBUTE = "buttons";
        private const string GAMEPAD_BUTTON_ELEMENT = "button";
        private readonly string NAMESPACE = "SprintZero1.Commands.PlayerCommands";
        private const string ACTION_COMMANDS_ELEMENT = "ActionCommands";
        private const string MENU_ACCESS_COMMANDS_ELEMENT = "MenuAccessCommands";
        private const string KEYBOARD_ELEMENT = "Keyboard";
        private const string GAMEPAD_ELEMENT = "GamePad";
        /* ----------------------------- Private Members ----------------------------- */
        private readonly XDocument _controllerDocument;
        private readonly XDocTools _parseTools;
        /* ----------------------------- Private Functions  ----------------------------- */
        /* ----------------------------- Public functions ----------------------------- */
        /// <summary>
        /// Create an object to help parse player inventory files
        /// </summary>
        /// <param name="controllerDocument">The document with the specific controller settings</param>
        /// <param name="rootName">The name of the root of the document</param>
        public PlayerControlsParser(XDocument controllerDocument, string rootName)
        {
            Debug.Assert(controllerDocument.Root != null || controllerDocument.Root.Name == rootName,
                $"Controller XML {controllerDocument} root is null or did not match {rootName}");
            _controllerDocument = controllerDocument;
            _parseTools = new XDocTools();
        }

        /// <summary>
        /// Parse The given elements for the keyboard controls used by the player
        /// </summary>
        /// <param name="keyboardElementString">The element name that contains all the elements and attributes for creating the controller map</param>
        /// <param name="player">The player who will use the commands</param>
        /// <param name="baseGameState">the game state that is affected by the commands</param>
        /// <returns></returns>
        public Dictionary<Keys, ICommand> ParseKeyboardControls(string keyboardElementString, ICombatEntity player, BaseGameState baseGameState)
        {
            /* Check to make sure the keyboard element exists */
            XElement keyboardElement = _controllerDocument.Root.Element(keyboardElementString);
            _parseTools.CheckIfElementNull(keyboardElement, keyboardElementString);

            XElement actionCommandsElement = keyboardElement.Element(ACTION_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(actionCommandsElement, ACTION_COMMANDS_ELEMENT);

            XElement menuAccessCommands = keyboardElement.Element(MENU_ACCESS_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(menuAccessCommands, MENU_ACCESS_COMMANDS_ELEMENT);

            /* Parse the file for commands that require the player and create the dictionary */
            Dictionary<Keys, ICommand> keyboardControlsMap = actionCommandsElement.Elements(KEYBOARD_KEY_ELEMENT).ToDictionary(
                    keyElement => _parseTools.ParseAttributeAsKeys(keyElement, KEYBOARD_KEYS_ATTRIBUTE),
                    keyElement => _parseTools.ParsePlayerActionCommands(keyElement, ACTION_ATTRIBUTE, NAMESPACE, player));


            /* Parse the file and add the menu access commands to the dictionary */
            foreach (XElement keyboardKeyElement in menuAccessCommands.Elements(KEYBOARD_KEY_ELEMENT))
            {
                Keys keyboardKey = _parseTools.ParseAttributeAsKeys(keyboardKeyElement, KEYBOARD_KEYS_ATTRIBUTE);
                ICommand playerCommand = _parseTools.ParsePlayerMenuCommands(keyboardKeyElement, ACTION_ATTRIBUTE, NAMESPACE, baseGameState);
                keyboardControlsMap.Add(keyboardKey, playerCommand);
            }


            return keyboardControlsMap;
        }

        public Dictionary<Buttons, ICommand> ParseGamePadControls(string gamePadElementString, ICombatEntity player, BaseGameState baseGameState)
        {
            /* Get the proper elements to parse */
            XElement gamePadElement = _controllerDocument.Root.Element(gamePadElementString);
            _parseTools.CheckIfElementNull(gamePadElement, gamePadElementString);

            XElement actionCommandsElement = gamePadElement.Element(ACTION_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(actionCommandsElement, ACTION_COMMANDS_ELEMENT);

            XElement menuAccessCommands = gamePadElement.Element(MENU_ACCESS_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(menuAccessCommands, MENU_ACCESS_COMMANDS_ELEMENT);
            Dictionary<Buttons, ICommand> keyboardControlsMap = actionCommandsElement.Elements(GAMEPAD_BUTTON_ELEMENT).ToDictionary(
                    keyElement => _parseTools.ParseAttributeAsButton(keyElement, GAMEPAD_BUTTONS_ATTRIBUTE),
                    keyElement => _parseTools.ParsePlayerActionCommands(keyElement, ACTION_ATTRIBUTE, NAMESPACE, player));
            /* Parse the file and add the menu access commands to the dictionary */
            foreach (XElement keyElement in menuAccessCommands.Elements(GAMEPAD_BUTTON_ELEMENT))
            {
                Buttons gamepadButton = _parseTools.ParseAttributeAsButton(keyElement, GAMEPAD_BUTTONS_ATTRIBUTE);
                ICommand playerCommand = _parseTools.ParsePlayerMenuCommands(keyElement, ACTION_ATTRIBUTE, NAMESPACE, baseGameState);
                keyboardControlsMap.Add(gamepadButton, playerCommand);
            }
            return keyboardControlsMap;
        }
    }
}

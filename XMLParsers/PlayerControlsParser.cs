using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
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
        private const string KeyboardKeyElement = "key";
        private const string KeyboardKeysAttribute = "keys";
        private const string ActionAttribute = "action";
        private const string GamepadButtonsAttribute = "buttons";
        private const string GamepadButtonElement = "button";
        private const string Namespace = "SprintZero1.Commands.PlayerCommands";
        private const string ActionCommandElement = "ActionCommands";
        private const string MenuAccessElement = "MenuAccessCommands";
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
        /// <returns></returns>
        public Dictionary<Keys, ICommand> ParseKeyboardControls(string keyboardElementString, ICombatEntity player, Game1 game)
        {
            /* Check to make sure the keyboard element exists */
            XElement keyboardElement = _controllerDocument.Root.Element(keyboardElementString);
            _parseTools.CheckIfElementNull(keyboardElement, keyboardElementString);

            XElement actionCommandsElement = keyboardElement.Element(ActionCommandElement);
            _parseTools.CheckIfElementNull(actionCommandsElement, ActionCommandElement);

            XElement menuAccessCommands = keyboardElement.Element(MenuAccessElement);
            _parseTools.CheckIfElementNull(menuAccessCommands, MenuAccessElement);

            /* Parse the file for commands that require the player and create the dictionary */
            Dictionary<Keys, ICommand> keyboardControlsMap = actionCommandsElement.Elements(KeyboardKeyElement).ToDictionary(
                    keyElement => _parseTools.ParseAttributeAsKeys(keyElement, KeyboardKeysAttribute),
                    keyElement => _parseTools.ParsePlayerActionCommands(keyElement, ActionAttribute, Namespace, player));


            /* Parse the file and add the menu access commands to the dictionary */
            foreach (XElement keyboardKeyElement in menuAccessCommands.Elements(KeyboardKeyElement))
            {
                Keys keyboardKey = _parseTools.ParseAttributeAsKeys(keyboardKeyElement, KeyboardKeysAttribute);
                ICommand playerCommand = _parseTools.ParsePlayerMenuCommands(keyboardKeyElement, ActionAttribute, Namespace, game);
                keyboardControlsMap.Add(keyboardKey, playerCommand);
            }
            return keyboardControlsMap;
        }

        /// <summary>
        /// Parses gamepad controls for the player controller
        /// </summary>
        /// <param name="gamePadElementString">The name of the element containing gamepad information</param>
        /// <param name="player">The player that the controller will belong to</param>
        /// <param name="game">The current game</param>
        /// <returns></returns>
        public Dictionary<Buttons, ICommand> ParseGamePadControls(string gamePadElementString, ICombatEntity player, Game1 game)
        {
            /* Get the proper elements to parse */
            XElement gamePadElement = _controllerDocument.Root.Element(gamePadElementString);
            _parseTools.CheckIfElementNull(gamePadElement, gamePadElementString);

            XElement actionCommandsElement = gamePadElement.Element(ActionCommandElement);
            _parseTools.CheckIfElementNull(actionCommandsElement, ActionCommandElement);

            XElement menuAccessCommands = gamePadElement.Element(MenuAccessElement);
            _parseTools.CheckIfElementNull(menuAccessCommands, MenuAccessElement);
            Dictionary<Buttons, ICommand> keyboardControlsMap = actionCommandsElement.Elements(GamepadButtonElement).ToDictionary(
                    keyElement => _parseTools.ParseAttributeAsButton(keyElement, GamepadButtonsAttribute),
                    keyElement => _parseTools.ParsePlayerActionCommands(keyElement, ActionAttribute, Namespace, player));
            /* Parse the file and add the menu access commands to the dictionary */
            foreach (XElement keyElement in menuAccessCommands.Elements(GamepadButtonElement))
            {
                Buttons gamepadButton = _parseTools.ParseAttributeAsButton(keyElement, GamepadButtonsAttribute);
                ICommand playerCommand = _parseTools.ParsePlayerMenuCommands(keyElement, ActionAttribute, Namespace, game);
                keyboardControlsMap.Add(gamepadButton, playerCommand);
            }
            return keyboardControlsMap;
        }
    }
}

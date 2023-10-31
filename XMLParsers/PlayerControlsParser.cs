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
    /// </summary>
    internal class PlayerControlsParser
    {
        /* ----------------------------- Attribute/Element strings ----------------------------- */
        private readonly string COMMAND_ELEMENT = "Command";
        private readonly string KEY_ELEMENT = "key";
        private readonly string KEYS_ATTRIBUTE = "keys";
        private readonly string ACTION_ATTRIBUTE = "action";
        private readonly string NAMESPACE = "SprintZero1.Commands.PlayerCommands";
        private const string PLAYER_ACTION_COMMANDS_ELEMENT = "PlayerActionCommands";
        private const string PLAYER_MENU_ACCESS_COMMANDS_ELEMENT = "PlayerMenuAccessCommands";
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
        /// <param name="elementName">The element name that contains all the elements and attributes for creating the controller map</param>
        /// <param name="player">The player who will use the commands</param>
        /// <param name="baseGameState">the game state that is affected by the commands</param>
        /// <returns></returns>
        public Dictionary<Keys, ICommand> ParseKeyboardControls(string elementName, ICombatEntity player, BaseGameState baseGameState)
        {
            XElement keyboardElement = _controllerDocument.Root.Element(elementName);
            _parseTools.CheckIfElementNull(keyboardElement, elementName);
            /* Sets up the player action commands */
            XElement playerActionElement = keyboardElement.Element(PLAYER_ACTION_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(playerActionElement, PLAYER_ACTION_COMMANDS_ELEMENT);
            Dictionary<Keys, ICommand> commandsMap = playerActionElement.Elements(COMMAND_ELEMENT).ToDictionary(
                    commandElement => _parseTools.ParseAttributeAsKeys(commandElement.Element(KEY_ELEMENT), KEYS_ATTRIBUTE),
                    commandElement => _parseTools.ParsePlayerActionCommands(commandElement.Element(KEY_ELEMENT), ACTION_ATTRIBUTE, NAMESPACE, player)
                );
            /* Set up the player menu access commands that affect game state - not the best implementation..*/
            XElement playerMenuAccessElement = keyboardElement.Element(PLAYER_MENU_ACCESS_COMMANDS_ELEMENT);
            _parseTools.CheckIfElementNull(playerMenuAccessElement, PLAYER_MENU_ACCESS_COMMANDS_ELEMENT);
            foreach (var menuCommandElement in playerMenuAccessElement.Elements(COMMAND_ELEMENT))
            {
                var keyElement = menuCommandElement.Element(KEY_ELEMENT);
                Keys key = _parseTools.ParseAttributeAsKeys(keyElement, KEYS_ATTRIBUTE);
                ICommand command = _parseTools.ParsePlayerMenuCommands(keyElement, ACTION_ATTRIBUTE, NAMESPACE, baseGameState);
                commandsMap.Add(key, command);
            }

            return commandsMap;

        }
    }
}

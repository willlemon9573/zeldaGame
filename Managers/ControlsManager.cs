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
        private static Dictionary<Keys, ICommand> _keyboardMap;

        /// <summary>
        /// Creates the keyboard controls map for the player to access if they are using keyboard controls
        /// </summary>
        /// <param name="player">The player using the keyboard</param>
        /// <param name="gameState">The current state of the game</param>
        public static void CreateKeyboardControls(IEntity player, IGameState gameState)
        {
            XDocument controllerDocument = XDocument.Load(@"XMLFiles\PlayerXMLFiles\ControllerSettings.xml");
            PlayerControlsParser controllerParser = new PlayerControlsParser(controllerDocument, "Controllers");
            _keyboardMap = controllerParser.ParseKeyboardControls("Keyboard", (ICombatEntity)player, (BaseGameState)gameState);
        }
        /// <summary>
        /// Gets the command map for keyboard controls
        /// </summary>
        /// <returns>the command map for keyboard controls</returns>
        public static Dictionary<Keys, ICommand> GetKeyboardControls()
        {
            return _keyboardMap;
        }

    }
}

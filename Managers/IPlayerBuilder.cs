using SprintZero1.Controllers;
using SprintZero1.Entities;
using System;

namespace SprintZero1.Managers
{
    internal interface IPlayerBuilder
    {
        /// <summary>
        /// Builds the desired character as a playerentity with a gamepad controller 
        /// </summary>
        /// <param name="characterName">The character the player controls (Link or Zelda)</param>
        /// <returns>A tuple that contains both the player entity and the controller they use</returns>
        Tuple<IEntity, IController> BuildPlayerWithKeyboard(string gamePadSettingsPath, Game1 game, string characterName);

        /// <summary>
        /// Builds the desired character as a playerentity with a keyboard controller 
        /// </summary>
        /// <param name="characterName">The character the player controls (Link or Zelda)</param>
        /// <returns>A tuple that contains both the player entity and the controller they use</returns>
        Tuple<IEntity, IController> BuildPlayerWithGamePad(string keyboardSettingsPath, Game1 game, string characterName);
    }
}

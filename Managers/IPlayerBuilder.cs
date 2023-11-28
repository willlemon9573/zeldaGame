using SprintZero1.Controllers;
using SprintZero1.Entities;
using System;

namespace SprintZero1.Managers
{
    internal interface IPlayerBuilder
    {
        /// <summary>
        /// Builds the desired player with keyboard controls after creation
        /// </summary>
        void BuildPlayerWithKeyboard();

        /// <summary>
        /// Builds the desired player with a gamepad controller
        /// </summary>
        void BuildPlayerWithGamePad();

        Tuple<IEntity, IController> GetPlayerAndController();
    }
}

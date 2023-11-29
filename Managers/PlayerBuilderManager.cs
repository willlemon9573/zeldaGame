using SprintZero1.Controllers;
using SprintZero1.Entities;
using System;

namespace SprintZero1.Managers
{
    internal class PlayerBuilderManager : IPlayerBuilder
    {
        private IEntity _player; // 102, 100


        public PlayerBuilderManager(string characterXMLPath, string characterName)
        {

        }

        public void BuildPlayerWithGamePad()
        {

        }

        public void BuildPlayerWithKeyboard()
        {

        }

        public Tuple<IEntity, IController> GetPlayerAndController()
        {
            return null;
        }
    }
}

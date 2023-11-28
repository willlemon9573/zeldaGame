using SprintZero1.Controllers;
using SprintZero1.Entities;
using System;

namespace SprintZero1.Managers
{
    internal class PlayerBuilderManager : IPlayerBuilder
    {
        IEntity _player; // 102, 100




        public PlayerBuilderManager(string characterXMLPath)
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
            throw new NotImplementedException();
        }
    }
}

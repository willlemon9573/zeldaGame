using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal abstract class BaseGameState : IGameState
    {
        protected readonly Game1 _game;
        /* Dictionary to contain each player with their specific controller. 
         * Indexed by the number that the player is. IE: Player One = 1, Player Two = 2, etc
         */
        protected Dictionary<int, Tuple<IEntity, IController>> _playerMap;
        protected BaseGameState(Game1 game)
        {
            _game = game;
            _playerMap = new Dictionary<int, Tuple<IEntity, IController>>();
        }

        public virtual void AddPlayer(Tuple<IEntity, IController> player)
        {
            int playerNumber = _playerMap.Count + 1; // start at 1 for player 1 and go up for the rest
            _playerMap.Add(playerNumber, player);
        }

        public virtual IEntity GetPlayer(int playerNumber)
        {
            return _playerMap[playerNumber].Item1;
        }

        public virtual void ChangeGameState(GameState newState)
        {
            GameStatesManager.ChangeGameState(newState);
        }

        public abstract void Handle();

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);


    }
}
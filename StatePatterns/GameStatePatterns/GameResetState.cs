﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameResetState : BaseGameState
    {
        /* Need to update these */
        private readonly List<Action> _resetList;
        public GameResetState(Game1 game) : base(game)
        {
            _resetList = new List<Action>()
            {
                { () => PlayerInventoryManager.Reset() },
                { () => ControlsManager.Reset() },
                { () => GameStatesManager.Reset() },
                { () => LevelManager.Reset() },
                { () => HUDManager.Reset() },
            };
        }

        public override void Draw(SpriteBatch spriteBatch) { }
        /// <summary>
        /// Handles resetting the game to its initial values
        /// </summary>
        public override void Handle()
        {
            foreach (var action in _resetList)
            {
                action();
            }

            _game.Reset();
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}

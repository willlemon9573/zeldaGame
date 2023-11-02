using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameResetState : BaseGameState
    {
        private List<Action> _resetList;
        public GameResetState(Game1 game) : base(game)
        {
            _resetList = new List<Action>()
            {
                { () => ColliderManager.Reset() },
                { () => ProgramManager.Reset() },
                { () => EntityManager.Reset() },
                { () => PlayerInventoryManager.Reset() },
                { () => ControlsManager.Reset() }

            };
        }

        public override void Draw(SpriteBatch spriteBatch) { }

        public override void Handle()
        {
            foreach (var action in _resetList)
            {
                action();
            }
            _game.GameState = GameStateFactory.GetGameState(GameState.Playing);
            LevelManager.Initialize(_game);
        }

        public override void Update(GameTime gameTime)
        {
            Handle();
        }
    }
}

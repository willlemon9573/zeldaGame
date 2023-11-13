using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal abstract class BaseGameState : IGameState
    {
        protected readonly Game1 _game;

        /// <summary>
        /// Entity Manager- Manager for all State entities
        /// </summary>
        public EntityManager EntityManager { get; private set; }

        /// <summary>
        /// List of Controllers to update
        /// </summary>
        public List<IController> Controllers { get; private set; }
        public BaseGameState(Game1 game)
        {
            _game = game;
            EntityManager = new EntityManager();
            Controllers = new List<IController>();
        }

        public virtual void AddController(IController controller)
        {
            Controllers.Add(controller);
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
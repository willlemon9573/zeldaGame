using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public abstract class BaseGameState : IGameState
    {

        protected readonly Game1 _game;
        public BaseGameState(Game1 game)
        {
            _game = game;
        }

        public virtual void ChangeGameState(GameState newState)
        {
            _game.GameState = GameStatesManager.GetGameState(newState);
        }

        public abstract void Handle();

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
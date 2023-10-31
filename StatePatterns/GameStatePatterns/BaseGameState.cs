using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.StatePatterns.StatePatternInterfaces;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public delegate void GameChangeStateHandler(GameState state);
    public delegate void GameStateHandler();
    public abstract class BaseGameState : IGameState
    {
        private readonly Game1 _game;
        public BaseGameState(Game1 game)
        {
            _game = game;
        }

        public abstract void Handle();

        public virtual void ChangeGameState(GameState newState)
        {
            _game.GameState = GameStateFactory.GetGameState(newState);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
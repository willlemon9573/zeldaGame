using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.GameStateMenu;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameOverState : BaseGameState
    {
        private readonly IGameStateMenu _gameOverMenu;

        public GameOverState(Game1 game) : base(game)
        {
            _gameOverMenu = new GameOverMenu(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _gameOverMenu.Draw(spriteBatch);
        }

        public override void Handle()
        {

        }

        public override void Update(GameTime gameTime)
        {
            _gameOverMenu.Update(gameTime);
        }
    }
}

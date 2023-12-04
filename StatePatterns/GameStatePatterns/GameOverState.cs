using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.GameStateMenu;
using System.Linq;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameOverState : BaseGameState
    {
        private readonly IGameStateMenu _gameOverMenu;
        private readonly ICommand _resetCommand;
        public GameOverState(Game1 game) : base(game)
        {
            _gameOverMenu = new GameOverMenu(game);
            _resetCommand = new ResetGameCommand();
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
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Contains(Keys.R))
            {
                _resetCommand.Execute();
            }

        }
    }
}

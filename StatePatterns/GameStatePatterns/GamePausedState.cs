using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Managers;
using System.Diagnostics;
using SprintZero1.GameStateMenu;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public class GamePausedState : BaseGameState
    {
        private PausedStateUpdater _updater;
        private IGameStateMenu pauseGame;
        public GamePausedState(Game1 game) : base(game)
        {
            pauseGame = new PauseMenu(game);
            _updater = ProgramManager.GetPausedStateUpdater();
            Debug.WriteLine("Game is paused");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ProgramManager.Draw(spriteBatch);
            pauseGame.Draw(spriteBatch);
        }

        public override void Handle()
        {
        }

        public override void Update(GameTime gameTime)
        {
            pauseGame.Update(gameTime);
            _updater.Invoke();
        }
    }
}

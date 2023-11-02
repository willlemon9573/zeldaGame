using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public class GamePausedState : BaseGameState
    {

        private IGameStateMenu pauseGame;
        public GamePausedState(Game1 game) : base(game)
        {
            pauseGame = new GameOverMenu(game);

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
        }
    }
}

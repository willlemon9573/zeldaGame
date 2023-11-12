using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public class GamePausedState : BaseGameState
    {
        private readonly PausedStateUpdater _updater;
        public GamePausedState(Game1 game) : base(game)
        {
            //_updater = ProgramManager.GetPausedStateUpdater();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //ProgramManager.Draw(spriteBatch);
            //drawing screen here
        }

        public override void Handle()
        {

        }

        public override void Update(GameTime gameTime)
        {
            //_updater.Invoke(_game);
        }
    }
}

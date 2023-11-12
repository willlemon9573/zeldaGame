using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GamePausedState : BaseGameState
    {
        private PausedStateUpdater _updater;
        public GamePausedState(Game1 game) : base(game)
        {
            
        }
        public override void AddController(IController controller)
        {
            KeyboardController k = controller as KeyboardController;
            _updater = k.PausedStateUpdate;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GameStatesManager.GetGameState(Enums.GameState.Playing).Draw(spriteBatch);
        }

        public override void Handle()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _updater.Invoke(_game);
        }
    }
}

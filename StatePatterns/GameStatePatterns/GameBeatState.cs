using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.GameStateMenu;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameBeatState : BaseGameState
    {
        private IGameStateMenu endingState;
        public GameBeatState(Game1 game) : base(game)
        {
            endingState = new GameBeatMenu(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Link should still be drawn
            endingState.Draw(spriteBatch);
        }

        public override void Handle()
        {

        }

        public override void Update(GameTime gameTime)
        {
            endingState.Update(gameTime);
        }
    }
}

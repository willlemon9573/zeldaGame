using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public class GamePausedState : BaseGameState
    {
        public GamePausedState(Game1 game) : base(game)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ProgramManager.Draw(spriteBatch);
        }

        public override void Handle()
        {
            // TODO: Implement switching keyboard controller
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

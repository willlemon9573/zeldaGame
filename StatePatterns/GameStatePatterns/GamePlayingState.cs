using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{

    public class GamePlayingState : BaseGameState
    {
        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game) { }
        /// <summary>
        /// Handles Drawing The entire game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            ProgramManager.Draw(spriteBatch);
        }

        public override void Handle()
        {

            // TODO: Update the player keyboard controls
        }
        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ProgramManager.Update(gameTime);
        }
    }
}

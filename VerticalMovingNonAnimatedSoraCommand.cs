
namespace SprintZero1
{
    public class VerticalMovingNonAnimatedSoraCommand : ICommand
    {
        private readonly Game1 myGame;

        /// <summary>
        /// Construct Command to change the current sprite to 
        /// a Vertical Moving Non Animated Sora Sprite
        /// </summary>
        /// <param name="game">The game object</param>
        public VerticalMovingNonAnimatedSoraCommand(Game1 game)
        {
            myGame = game;
        }
        public void Execute()
        {
            myGame.SetSprite(new VerticalMovingNonAnimatedSora());
        }
    }
}

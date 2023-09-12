
namespace SprintZero1
{
    public class MovingInPlaceSoraAnimatedCommand : ICommand
    {
        private readonly Game1 myGame;
        /// <summary>
        /// Construct Command to change the current sprite to 
        /// a Moving In Place Sora Sprite object
        /// </summary>
        /// <param name="game">The game object</param>
        public MovingInPlaceSoraAnimatedCommand(Game1 myGame)
        {
            this.myGame = myGame;
        }
        public void Execute()
        {
            myGame.SetSprite(new MovingInPlaceSoraAnimated());
        }
    }
}

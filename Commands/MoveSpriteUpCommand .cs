namespace SprintZero1.Commands
{
    public class MoveSpriteUpCommand : ICommand
    {
        private readonly UpMovingSprite sprite;

        public MoveSpriteUpCommand(UpMovingSprite sprite)
        {
            this.sprite = sprite;
        }

        public void Execute()
        {
            sprite.MoveAndChangeFrame(/* timeInSeconds value here */);
        }
    }
}

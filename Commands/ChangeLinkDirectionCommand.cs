using SprintZero1.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SprintZero1.Commands
{
    public class ChangeLinkDirectionCommand : ICommand
    {
        private Game1 game;
        private int newDirection;
        private Vector2 location;
        private int[] boundaries = new int[] { 300, 500 };

        public ChangeLinkDirectionCommand(Game1 game, int direction)
        {
            this.game = game;
            this.newDirection = direction;
        }

        public void Execute()
        {
           /* location = game.positoin;
            switch (newDirection)
            {
                case 0: // Moving Upwards
                    location.Y -= speed * timeInSeconds;
                    if (location.Y <= boundaries[0] || location.Y >= boundaries[1])
                    {
                        location.Y += speed * timeInSeconds;
                    }
                    break;

                case 1: // Moving Downwards
                    location.Y += speed * timeInSeconds;
                    if (location.Y <= boundaries[0] || location.Y >= boundaries[1])
                    {
                        location.Y -= speed * timeInSeconds;
                    }
                    break;

                case 2: // Moving Left
                    location.X -= speed * timeInSeconds;
                    if (location.X <= boundaries[2] || location.X >= boundaries[3])
                    {
                        location.X += speed * timeInSeconds;
                    }
                    break;

                case 3: // Moving Right
                    location.X += speed * timeInSeconds;
                    if (location.X <= boundaries[2] || location.X >= boundaries[3])
                    {
                        location.X -= speed * timeInSeconds;
                    }
                    break;

                default:
                    // Handle other directions if necessary
                    break;
            }

            // Update animation frame
            timeElapsed += timeInSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                currentFrame++;
                if (currentFrame > totalFrames)
                {
                    currentFrame = 1;
                }
            }
            game.positoin = location;*/
            /*ISprite newSprite = LinkFactory.createNewLink(newDirection, location);
            game.Link = newSprite;*/
        }
    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Factories;
namespace SprintZero1.Commands
{
    public class ChangeLinkDirectionCommand : ICommand
    {
        private Game1 game;
        private int newDirection;
        private Vector2 location;
        private ILinkFactory _linkFactory;
        private bool isAttacking;
        private int[] boundaries = new int[] { 0, 440, 0, 750 };

        public ChangeLinkDirectionCommand(Game1 game, int direction)
        {
            this.game = game;
            this.newDirection = direction;
            // this._linkFactory = game.linkFactory;
            isAttacking = false;
        }
        public void Execute()
        {
            /* location = game.position;

             switch (newDirection)
             {
                 case 0: // Moving Upwards
                     location.Y -= 5;
                     if (location.Y < boundaries[0])
                         location.Y = boundaries[0];
                     break;

                 case 1: // Moving Downwards
                     location.Y += 5;
                     if (location.Y > boundaries[1])
                         location.Y = boundaries[1];
                     break;

                 case 2: // Moving Left
                     location.X -= 5;
                     if (location.X < boundaries[2])
                         location.X = boundaries[2];
                     break;

                 case 3: // Moving Right
                     location.X += 5;
                     if (location.X > boundaries[3])
                         location.X = boundaries[3];
                     break;

                 default:
                     // Handle other directions if necessary
                     break;
             }

             game.position = location;  // Make sure Game1's position property has a public setter.

             // Check if the current direction is the same as the new direction
             if (game.CurrentDirection == newDirection)
             {
                 // Toggle the frame
                 game.CurrentFrame = (game.CurrentFrame == 0) ? 1 : 0;
             }
             else
             {
                 // Set the frame to 0 and update the direction
                 game.CurrentFrame = 0;
                 game.CurrentDirection = newDirection;
             }

             // Create a new Link sprite using the new direction, location, and frame
             ISprite newSprite = _linkFactory.createNewLink(newDirection, location, game.CurrentFrame, isAttacking);

             // Set the new Link sprite in the game
             game.SetLink(newSprite);*/
        }
    }
}


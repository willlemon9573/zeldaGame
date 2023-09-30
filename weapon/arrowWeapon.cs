using Microsoft.Xna.Framework;

namespace SprintZero1.weapon
{

    public class arrowWeapon
    {
        private Vector2 location;
        private int direction;
        private float speed;
        private float distanceMoved = 0;
        private bool returning = false;
        public bool IsActive { get; private set; } = true;

        public arrowWeapon(Game1 game)
        {
            this.location = game.position;
            this.direction = game.direction;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive)
                return;

            if (returning)
            {
                MoveArrow(-speed);
            }
            else
            {
                MoveArrow(speed);
            }

            distanceMoved += speed;

            if (distanceMoved >= 20 && !returning)
            {
                returning = true;
                distanceMoved = 0;
            }
            else if (distanceMoved >= 20 && returning)
            {
                IsActive = false; 
            }
        }

        private void MoveArrow(float moveSpeed)
        {
            switch (direction)
            {
                case 0: // Moving Upwards
                    location.Y -= moveSpeed;
                    break;
                case 1: // Moving Downwards
                    location.Y += moveSpeed;
                    break;
                case 2: // Moving Left
                    location.X -= moveSpeed;
                    break;
                case 3: // Moving Right
                    location.X += moveSpeed;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }
        }
    }

}
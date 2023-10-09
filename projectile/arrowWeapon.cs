using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.weapon
{

    internal class arrowWeapon
    {
        private Vector2 location;
        private int direction;
        private float speed;
        private float distanceMoved = 0;
        private bool returning = false;
        public bool IsActive { get; private set; } = true;
        IEntity _projectile;

        public arrowWeapon(IEntity ProjectileEntity)
        {
            _projectile = ProjectileEntity;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive)
                return;

            if (returning)
            {
                MoveProjectile(-speed);
            }
            else
            {
                MoveProjectile(speed);
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

        private void MoveProjectile(float moveSpeed)
        {
            Direction direction = _projectile._projectileDirection;
            Vector2 location = _projectile._projectilePosition;
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
            _projectile._projectilePosition = location;
        }
    }

}
using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{

    internal class comingBackProjectile : IProjectile
    {
        private Vector2 location;
        private int direction;
        private float speed = 3;
        private float distanceMoved = 0;
        private bool returning = false;
        public bool IsActive { get; private set; } = true;
        private int _maxDistance;
        ProjectileEntity _projectile;

        public comingBackProjectile(ProjectileEntity ProjectileEntity, int maxDistance)
        {
            _projectile = ProjectileEntity;
            _maxDistance = maxDistance;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                _projectile.projectileSprite = null;
                _projectile.projectileUpdate = null;
                return;
            }
            if (returning)
            {
                MoveProjectile(-speed);
            }
            else
            {
                MoveProjectile(speed);
            }

            distanceMoved += speed;

            if (distanceMoved >= 50 && !returning)
            {
                returning = true;
                distanceMoved = 0;
            }
            else if (distanceMoved >= 50 && returning)
            {
                IsActive = false;
            }
        }

        private void MoveProjectile(float moveSpeed)
        {
            Direction direction = _projectile.Direction;
            Vector2 location = _projectile.Position; 
            switch (direction)
            {
                case Direction.North: // Moving Upwards
                    location.Y -= moveSpeed;
                    break;
                case Direction.South: // Moving Downwards
                    location.Y += moveSpeed;
                    break;
                case Direction.West: // Moving Left
                    location.X -= moveSpeed;
                    break;
                case Direction.East: // Moving Right
                    location.X += moveSpeed;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }
            _projectile.Position = location;
        }
    }

}
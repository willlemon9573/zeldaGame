using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{

    internal class comingBackProjectile : IProjectile
    {
        private Vector2 location;
        private int direction;
        private float _speed;
        private float distanceMoved = 0;
        private bool returning = false;
        public bool IsActive { get; private set; } = true;
        private int _maxDistance;
        private float RotationIncrement = MathHelper.ToRadians(10);
        ProjectileEntity _projectile;

        public comingBackProjectile(ProjectileEntity ProjectileEntity, int maxDistance, float speed)
        {
            _projectile = ProjectileEntity;
            _maxDistance = maxDistance;
            _speed = speed;
        }

        public void Update(GameTime gameTime)
        {

            _projectile.Rotation += RotationIncrement;

            if (_projectile.Rotation > MathHelper.TwoPi)
                _projectile.Rotation -= MathHelper.TwoPi;
            if (!IsActive)
            {
                _projectile.projectileSprite = null;
                _projectile.projectileUpdate = null;
                return;
            }
            if (returning)
            {
                MoveProjectile(-_speed);
            }
            else
            {
                MoveProjectile(_speed);
            }

            distanceMoved += _speed;

            if (distanceMoved >= _maxDistance && !returning)
            {
                returning = true;
                distanceMoved = 0;
            }
            else if (distanceMoved >= _maxDistance && returning)
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
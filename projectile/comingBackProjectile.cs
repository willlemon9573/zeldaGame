using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{
    internal class comingBackProjectile : IProjectile
    {
        private Vector2 location; // Current location of the projectile
        private int direction; // Direction in which the projectile is moving
        private float _speed; // Speed at which the projectile moves
        private float distanceMoved = 0; // Distance traveled by the projectile
        private bool returning = false; // Flag to indicate if the projectile is returning
        public bool IsActive { get; private set; } = true; // Flag to indicate if the projectile is active
        private int _maxDistance; // Maximum distance the projectile can travel before becoming inactive
        private float RotationIncrement = MathHelper.ToRadians(10); // Increment for projectile rotation
        ProjectileEntity _projectile; // Reference to the projectile entity

        public comingBackProjectile(ProjectileEntity ProjectileEntity, int maxDistance, float speed)
        {
            _projectile = ProjectileEntity; // Initialize the projectile with the provided entity
            _maxDistance = maxDistance; // Set the maximum distance the projectile can travel
            _speed = speed; // Set the speed of the projectile
        }

        public void Update(GameTime gameTime)
        {
            _projectile.Rotation += RotationIncrement;

            if (_projectile.Rotation > MathHelper.TwoPi)
                _projectile.Rotation -= MathHelper.TwoPi;

            if (!IsActive)
            {
                // If the projectile is not active, set its sprite and update function to null
                _projectile.projectileSprite = null;
                _projectile.projectileUpdate = null;
                return;
            }

            if (returning)
            {
                MoveProjectile(-_speed); // Move the projectile back if it's returning
            }
            else
            {
                MoveProjectile(_speed); // Move the projectile forward
            }

            distanceMoved += _speed;

            if (distanceMoved >= _maxDistance && !returning)
            {
                returning = true; // Start returning when the maximum distance is reached
                distanceMoved = 0;
            }
            else if (distanceMoved >= _maxDistance && returning)
            {
                IsActive = false; // Deactivate the projectile when it returns and reaches max distance
            }
        }

        private void MoveProjectile(float moveSpeed)
        {
            Direction direction = _projectile.Direction;
            Vector2 location = _projectile.Position;

            // Calculate the new position of the projectile based on its direction and speed
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

            _projectile.Position = location; // Update the position of the projectile
        }
    }
}

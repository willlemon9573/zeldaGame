using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.projectile
{
    internal class NotcomingBackProjectile : IProjectile
    {
        private double timer = 0; // Timer to track how long the projectile has been active
        private double waitingTime = 50; // The time in milliseconds before the projectile ends
        private Vector2 location; // Current location of the projectile
        private int direction; // Direction in which the projectile is moving
        private float _speed; // Speed at which the projectile moves
        private float distanceMoved = 0; // Distance traveled by the projectile
        public bool IsActive = true; // Flag to indicate if the projectile is active
        private int _maxDistance; // Maximum distance the projectile can travel before becoming inactive
        private bool shouldEnd = false; // Flag to indicate if the projectile should end
        ProjectileEntity _projectile; // Reference to the projectile entity

        public NotcomingBackProjectile(ProjectileEntity ProjectileEntity, int maxDistance, float speed)
        {
            _projectile = ProjectileEntity; // Initialize the projectile with the provided entity
            _maxDistance = maxDistance; // Set the maximum distance the projectile can travel
            _speed = speed; // Set the speed of the projectile
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                _projectile.projectileSprite = _projectile.endingSprite;
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer >= waitingTime)
                {
                    _projectile.projectileSprite = null;
                    _projectile.projectileUpdate = null;
                    timer = 0;
                }
                return;
            }

            MoveProjectile(_speed);
            distanceMoved += _speed;

            if (distanceMoved >= _maxDistance)
            {
                IsActive = false; // Deactivate the projectile when it reaches max distance
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

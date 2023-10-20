using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using SprintZero1.Sprites;

namespace SprintZero1.Commands
{
    internal class FireBoomerangCommand : ICommand
    {
        private Direction Direction; // Direction in which the boomerang will be thrown
        private Vector2 startLocation; // Starting location of the boomerang
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who throws the boomerang
        private float launchOffset = 15; // Offset distance from the player's position to start the boomerang
        private float movingSpeed = 2.5f; // Speed at which the boomerang moves
        private int maxDistance = 50; // Maximum distance the boomerang can travel
        ISprite newSprite; // Sprite for the boomerang
        ProjectileEntity _Entity; // Entity representing the boomerang
        IProjectile _projectileType; // Type of projectile (e.g., coming back or not)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        /// <summary>
        /// Initializes a new instance of the FireBoomerangCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who throws the boomerang.</param>
        /// <param name="ProjectileEntity">The entity representing the boomerang projectile.</param>
        public FireBoomerangCommand(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            startLocation = _PlayerEntity.Position;
            IMovableEntity _PlayerEntityMoveable = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerEntityMoveable.Direction; // Get the direction the player is facing

            // Calculate the starting location of the boomerang based on the player's direction
            switch (Direction)
            {
                case Direction.North:
                    startLocation.Y -= launchOffset;
                    break;
                case Direction.South:
                    startLocation.Y += launchOffset;
                    break;
                case Direction.West:
                    startLocation.X -= launchOffset;
                    break;
                case Direction.East:
                    startLocation.X += launchOffset;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }

            spriteEffect = SpriteEffects.None;
            _Entity._ChangeSpriteEffects = spriteEffect;

            // Create a new boomerang sprite based on the direction
            newSprite = WeaponFactory.CreateBoomerangSprite("", Direction);

            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _Entity.endingSprite = null;

            _projectileType = new BoomerangWeaponProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;
        }
    }
}

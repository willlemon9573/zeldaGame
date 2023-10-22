using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class FireArrowCommand : ICommand
    {
        private Direction Direction; // Direction in which the arrow will be fired
        private Vector2 startLocation; // Starting location of the arrow
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who fires the arrow
        private float launchOffset = 15; // Offset distance from the player's position to start the arrow
        private float movingSpeed = 2; // Speed at which the arrow moves
        private int maxDistance = 40; // Maximum distance the arrow can travel before disappearing
        public ISprite newSprite; // Sprite for the arrow
        public IProjectileEntity _Entity; // Entity representing the arrow
        public IProjectile _projectileType; // Type of projectile (e.g., non-returning arrow)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        /// <summary>
        /// Initializes a new instance of the FireArrowCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who fires the arrow.</param>
        /// <param name="ProjectileEntity">The entity representing the arrow projectile.</param>
        public FireArrowCommand(IEntity PlayerEntity, IEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = (IProjectileEntity)ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            startLocation = _PlayerEntity.Position;
            IMovableEntity _PlayerMovableEntity = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerMovableEntity.Direction; // Get the direction the player is facing

            // Calculate the starting location of the arrow and set sprite effects based on direction
            switch (Direction)
            {
                case Direction.North:
                    startLocation.Y -= launchOffset;
                    spriteEffect = SpriteEffects.None;
                    break;
                case Direction.South:
                    startLocation.Y += launchOffset;
                    spriteEffect = SpriteEffects.FlipVertically;
                    break;
                case Direction.West:
                    startLocation.X -= launchOffset;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.East:
                    startLocation.X += launchOffset;
                    spriteEffect = SpriteEffects.None;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }

            _Entity.Rotation = 0;
            _Entity._ChangeSpriteEffects = spriteEffect;

            // Create a new arrow sprite based on the direction
            newSprite = WeaponFactory.CreateArrowSprite("", Direction);

            // Create an ending sprite for the arrow
            _Entity.EndingSprite = WeaponFactory.CreateEndSprite();

            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.ProjectileSprite = newSprite;

            // Create a non-returning arrow projectile type and assign it to the entity
            _projectileType = new NonComingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.ProjectileUpdate = _projectileType;
        }
    }
}

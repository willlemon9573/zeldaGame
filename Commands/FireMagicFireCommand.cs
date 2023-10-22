using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class FireMagicFireCommand : ICommand
    {
        private Direction Direction; // Direction in which the magic fire projectile will be shot
        private Vector2 startLocation; // Starting location of the magic fire projectile
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who shoots the magic fire
        private float launchOffset = 15; // Offset distance from the player's position to start the magic fire projectile
        private float movingSpeed = 1f; // Speed at which the magic fire projectile moves
        private int maxDistance = 30; // Maximum distance the magic fire projectile can travel before disappearing
        public ISprite newSprite; // Sprite for the arrow
        public IProjectileEntity _Entity; // Entity representing the arrow
        public IProjectile _projectileType; // Type of projectile (e.g., non-returning arrow)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        /// <summary>
        /// Initializes a new instance of the FireMagicFireCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who shoots the magic fire.</param>
        /// <param name="ProjectileEntity">The entity representing the magic fire projectile.</param>
        public FireMagicFireCommand(IEntity PlayerEntity, IEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = (IProjectileEntity)ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            startLocation = _PlayerEntity.Position;
            IMovableEntity _PlayerMovableEntity = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerMovableEntity.Direction;

            // Calculate the starting location of the magic fire projectile based on the player's direction
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

            _Entity.Rotation = 0;
            spriteEffect = SpriteEffects.None;
            _Entity._ChangeSpriteEffects = spriteEffect;

            // Create a new magic fire sprite
            newSprite = WeaponFactory.CreateMagicFireSprite();

            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.EndingSprite = null; // No specific ending sprite for magic fire
            _Entity.ProjectileSprite = newSprite;

            _projectileType = new NonComingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.ProjectileUpdate = _projectileType;
        }
    }
}

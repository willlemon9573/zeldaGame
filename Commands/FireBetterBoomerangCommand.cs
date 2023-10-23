using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using SprintZero1.Sprites;

namespace SprintZero1.Commands
{
    internal class FireBetterBoomerangCommand : ICommand
    {
        private Direction Direction; // Direction of the projectile
        private Vector2 startLocation; // Starting location of the projectile
        private readonly WeaponSpriteFactory WeaponFactory; // Factory used to create weapon sprites
        private readonly IEntity _PlayerEntity; // Entity to which the projectile belongs
        private readonly float launchOffset = 15; // Offset from the entity's position where the projectile is launched
        private readonly float movingSpeed = 3.5f; // Speed at which the projectile moves
        private readonly int maxDistance = 70; // Maximum distance the projectile can travel
        public ISprite newSprite; // Sprite for the arrow
        public IProjectileEntity _Entity; // Entity representing the arrow
        public IProjectile _projectileType; // Type of projectile (e.g., non-returning arrow)
        private SpriteEffects spriteEffect; // Sprite effect


        /// <summary>
        /// Initializes a new instance of the FireBetterBoomerangCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who fires the better boomerang.</param>
        /// <param name="ProjectileEntity">The entity representing the better boomerang projectile.</param>
        public FireBetterBoomerangCommand(IEntity PlayerEntity, IEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = (IProjectileEntity)ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            startLocation = _PlayerEntity.Position;
            IMovableEntity _PlayerMovableEntity = (IMovableEntity)_PlayerEntity;
            ICombatEntity _PlayerCombatEntity = (ICombatEntity)_PlayerEntity;
            _PlayerCombatEntity.Attack("boomerang");
            Direction = _PlayerMovableEntity.Direction; // Get the direction the player is facing

            // Calculate the starting location of the better boomerang and set sprite effects based on direction
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
            newSprite = WeaponFactory.CreateBoomerangSprite("better");
            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.ProjectileSprite = newSprite;
            _Entity.EndingSprite = null;

            // Create a better boomerang projectile type and assign it to the entity
            _projectileType = new BoomerangWeaponProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.ProjectileUpdate = _projectileType;
        }
    }
}

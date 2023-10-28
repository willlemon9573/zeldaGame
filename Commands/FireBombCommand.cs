using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using SprintZero1.Sprites;

namespace SprintZero1.Commands
{
    internal class FireBombCommand : ICommand
    {
        private Direction Direction; // Direction in which the bomb will be thrown
        private Vector2 startLocation; // Starting location of the bomb
        private readonly WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private readonly IEntity _PlayerEntity; // The player entity who throws the bomb
        private readonly float launchOffset = 15; // Offset distance from the player's position to start the bomb
        public ISprite newSprite; // Sprite for the arrow
        public IProjectileEntity _Entity; // Entity representing the arrow
        public IProjectile _projectileType; // Type of projectile (e.g., non-returning arrow)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        /// <summary>
        /// Initializes a new instance of the FireBombCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who throws the bomb.</param>
        /// <param name="ProjectileEntity">The entity representing the bomb projectile.</param>
        public FireBombCommand(IEntity PlayerEntity, IEntity ProjectileEntity)
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
            _PlayerCombatEntity.Attack("bomb");
            Direction = _PlayerMovableEntity.Direction; // Get the direction the player is facing

            // Calculate the starting location of the bomb based on the player's direction
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

            // Create a new bomb sprite
            newSprite = WeaponFactory.CreateBombSprite();

            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.ProjectileSprite = newSprite;

            // Create a bomb projectile type and assign it to the entity
            _projectileType = new BombProjectile(_Entity);
            _Entity.ProjectileUpdate = _projectileType;

            _Entity.EndingSprite = WeaponFactory.CreateBombSpriteExplodes();
        }
    }
}

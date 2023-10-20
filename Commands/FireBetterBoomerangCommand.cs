using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class FireBetterBoomerangCommand : ICommand
    {
        private Direction Direction;
        private Vector2 startLocation;
        private WeaponSpriteFactory WeaponFactory;
        private IEntity _PlayerEntity;
        private float launchOffset = 15;
        private float movingSpeed = 3.5f;
        private int maxDistance = 70;
        ISprite newSprite;
        ProjectileEntity _Entity;
        IProjectile _projectileType;
        private SpriteEffects spriteEffect;

        /// <summary>
        /// Initializes a new instance of the FireBetterBoomerangCommand class.
        /// </summary>
        /// <param name="PlayerEntity">The player entity who fires the better boomerang.</param>
        /// <param name="ProjectileEntity">The entity representing the better boomerang projectile.</param>
        public FireBetterBoomerangCommand(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            startLocation = _PlayerEntity.Position;
            IMovableEntity _PlayerEntityMoveable = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerEntityMoveable.Direction;

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
            newSprite = WeaponFactory.CreateBoomerangSprite("better", Direction);
            _Entity.Position = startLocation;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _Entity.endingSprite = null;

            // Create a better boomerang projectile type and assign it to the entity
            _projectileType = new BoomerangWeaponProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;
        }
    }
}

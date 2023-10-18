using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class ArrowWeapon : ICommand
    {
        private Direction Direction; // Direction in which the arrow will be fired
        private Vector2 location; // Starting location of the arrow
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who fires the arrow
        private int howfarFront = 15; // Distance from the player's position to start the arrow
        private float movingSpeed = 2; // Speed at which the arrow moves
        private int maxDistance = 40; // Maximum distance the arrow can travel before disappearing
        ISprite newSprite; // Sprite for the arrow
        ProjectileEntity _Entity; // Entity representing the arrow
        IProjectile _projectileType; // Type of projectile (e.g., non-returning arrow)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        public ArrowWeapon(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            location = _PlayerEntity.Position;
            IMovableEntity _PlayerEntityMoveable = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerEntityMoveable.Direction;

            // Calculate the starting location of the arrow and set sprite effects based on direction
            switch (Direction)
            {
                case Direction.North: // Moving Upwards
                    location.Y -= howfarFront;
                    spriteEffect = SpriteEffects.None;
                    break;
                case Direction.South: // Moving Downwards
                    location.Y += howfarFront;
                    spriteEffect = SpriteEffects.FlipVertically;
                    break;
                case Direction.West: // Moving Left
                    location.X -= howfarFront;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.East: // Moving Right
                    location.X += howfarFront;
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
            _Entity.endingSprite = WeaponFactory.CreateEndSprite();

            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;

            // Create a non-returning arrow projectile type and assign it to the entity
            _projectileType = new NotcomingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;
        }
    }
}

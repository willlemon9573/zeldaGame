using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class BoomerangWeapon : ICommand
    {
        private Direction Direction; // Direction in which the boomerang will be thrown
        private Vector2 location; // Starting location of the boomerang
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who throws the boomerang
        private int howfarFront = 15; // Distance from the player's position to start the boomerang
        private float movingSpeed = 2.5f; // Speed at which the boomerang moves
        private int maxDistance = 50; // Maximum distance the boomerang can travel
        ISprite newSprite; // Sprite for the boomerang
        ProjectileEntity _Entity; // Entity representing the boomerang
        IProjectile _projectileType; // Type of projectile (e.g., coming back or not)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        public BoomerangWeapon(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
        {
            _PlayerEntity = PlayerEntity;
            _Entity = ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {
            location = _PlayerEntity.Position;
            IMovableEntity _PlayerEntityMoveable = (IMovableEntity)_PlayerEntity;
            Direction = _PlayerEntityMoveable.Direction; // Get the direction the player is facing

            // Calculate the starting location of the boomerang based on the player's direction
            switch (Direction)
            {
                case Direction.North: // Moving Upwards
                    location.Y -= howfarFront;
                    break;
                case Direction.South: // Moving Downwards
                    location.Y += howfarFront;
                    break;
                case Direction.West: // Moving Left
                    location.X -= howfarFront;
                    break;
                case Direction.East: // Moving Right
                    location.X += howfarFront;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }

            spriteEffect = SpriteEffects.None;
            _Entity._ChangeSpriteEffects = spriteEffect;

            // Create a new boomerang sprite based on the direction
            newSprite = WeaponFactory.CreateBoomerangSprite("", Direction);

            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _Entity.endingSprite = null;

            // Create a new projectile type (e.g., coming back boomerang) and assign it to the entity
            _projectileType = new comingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;
        }
    }
}

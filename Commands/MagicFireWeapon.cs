using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class MagicFireWeapon : ICommand
    {
        private Direction Direction; // Direction in which the magic fire projectile will be shot
        private Vector2 location; // Starting location of the magic fire projectile
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who shoots the magic fire
        private int howfarFront = 15; // Distance from the player's position to start the magic fire projectile
        private float movingSpeed = 1f; // Speed at which the magic fire projectile moves
        private int maxDistance = 30; // Maximum distance the magic fire projectile can travel before disappearing
        ISprite newSprite; // Sprite for the magic fire projectile
        ProjectileEntity _Entity; // Entity representing the magic fire projectile
        IProjectile _projectileType; // Type of projectile (e.g., non-returning magic fire)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        public MagicFireWeapon(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
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

            // Calculate the starting location of the magic fire projectile based on the player's direction
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

            _Entity.Rotation = 0;
            spriteEffect = SpriteEffects.None;
            _Entity._ChangeSpriteEffects = spriteEffect;

            // Create a new magic fire sprite
            newSprite = WeaponFactory.CreateMagicFireSprite();

            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.endingSprite = null; // No specific ending sprite for magic fire
            _Entity.projectileSprite = newSprite;

            // Create a non-returning magic fire projectile type and assign it to the entity
            _projectileType = new NotcomingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;
        }
    }
}

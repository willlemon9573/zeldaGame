using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Commands
{
    internal class BombWeapon : ICommand
    {
        private Direction Direction; // Direction in which the bomb will be thrown
        private Vector2 location; // Starting location of the bomb
        private WeaponSpriteFactory WeaponFactory; // Factory for creating weapon sprites
        private IEntity _PlayerEntity; // The player entity who throws the bomb
        private int howfarFront = 15; // Distance from the player's position to start the bomb
        ISprite newSprite; // Sprite for the bomb
        ProjectileEntity _Entity; // Entity representing the bomb
        IProjectile _projectileType; // Type of projectile (e.g., bomb)
        private SpriteEffects spriteEffect; // Sprite effects for rendering

        public BombWeapon(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
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

            // Calculate the starting location of the bomb based on the player's direction
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

            // Create a new bomb sprite
            newSprite = WeaponFactory.CreateBombSprite();

            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;

            // Create a bomb projectile type and assign it to the entity
            _projectileType = new bombProjectile(_Entity);
            _Entity.projectileUpdate = _projectileType;

            // Create an ending sprite for the bomb (e.g., explosion sprite)
            _Entity.endingSprite = WeaponFactory.CreateBombSpriteExplodes();
        }
    }
}

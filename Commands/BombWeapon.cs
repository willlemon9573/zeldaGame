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
        private Direction Direction;
        private Vector2 location;
        private WeaponSpriteFactory WeaponFactory;
        private IEntity _PlayerEntity;
        private int moveSpeed = 15;
        ISprite newSprite;
        ProjectileEntity _Entity;
        IProjectile _projectileType;
        private SpriteEffects spriteEffect;

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
            switch (Direction)
            {
                case Direction.North: // Moving Upwards
                    location.Y -= moveSpeed;
                    break;
                case Direction.South: // Moving Downwards
                    location.Y += moveSpeed;
                    break;
                case Direction.West: // Moving Left
                    location.X -= moveSpeed;
                    break;
                case Direction.East: // Moving Right
                    location.X += moveSpeed;
                    break;
                default:
                    // Handle other directions if necessary
                    break;
            }
            _Entity.Rotation = 0;
            spriteEffect = SpriteEffects.None;
            _Entity._ChangeSpriteEffects = spriteEffect;
            newSprite = WeaponFactory.CreateBombSprite();
            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _Entity.endingSprite = WeaponFactory.CreateBombSpriteExplodes();
            _projectileType = new bombProjectile(_Entity);
            _Entity.projectileUpdate = _projectileType;
            //location = game.position;
            // Direction = game.CurrentDirection;
            //newSprite = WeaponFactory.CreateBombSprite(location, 4, -1);
            //game.Weapon = newSprite;
        }

    }
}

using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;

namespace SprintZero1.Commands 
{
    internal class ArrowWeapon : ICommand
    {
        private Direction Direction;
        private Vector2 location;
        private WeaponSpriteFactory WeaponFactory;
        private IEntity _PlayerEntity;
        ISprite newSprite;
        ProjectileEntity _Entity;
        IProjectile _projectileType;

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
            newSprite = WeaponFactory.CreateArrowSprite("", location, Direction);
            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _projectileType = new arrowProjectile(_Entity);
            _Entity.projectileUpdate = _projectileType;
            // location = game.position;
            // Direction = game.CurrentDirection;
            // newSprite = WeaponFactory.CreateArrowSprite("", location, 3, Direction);
            // game.Weapon = newSprite;
        }

    }
}

﻿using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using Microsoft.Xna.Framework.Graphics;
namespace SprintZero1.Commands
{
    internal class betterArrowWeapon : ICommand
    {
        private Direction Direction;
        private Vector2 location;
        private WeaponSpriteFactory WeaponFactory;
        private IEntity _PlayerEntity;
        private int howfarFront = 15;
        private float movingSpeed = 3;
        private int maxDistance = 60;
        ISprite newSprite;
        ProjectileEntity _Entity;
        IProjectile _projectileType;
        private SpriteEffects spriteEffect;

        public betterArrowWeapon(IEntity PlayerEntity, ProjectileEntity ProjectileEntity)
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
            newSprite = WeaponFactory.CreateArrowSprite("better",  Direction);
            _Entity.endingSprite = WeaponFactory.CreateEndSprite();
            _Entity.Position = location;
            _Entity.Direction = Direction;
            _Entity.projectileSprite = newSprite;
            _projectileType = new NotcomingBackProjectile(_Entity, maxDistance, movingSpeed);
            _Entity.projectileUpdate = _projectileType;

        }

    }
}

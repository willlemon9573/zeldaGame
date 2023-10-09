﻿using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;

namespace SprintZero1.Commands
{
    internal class ArrowWeapon : ICommand
    {
        private Direction Direction;
        private Vector2 location;
        private WeaponSpriteFactory WeaponFactory;
        ISprite newSprite;
        IEntity _Entity;

        public ArrowWeapon(IEntity PlayerEntity, IEntity ProjectileEntity)
        {
            location = PlayerEntity._playerDirection;
            Direction = PlayerEntity._playerDirection;
            _Entity = ProjectileEntity;
            this.WeaponFactory = WeaponSpriteFactory.Instance;
        }

        public void Execute()
        {

            newSprite = WeaponFactory.CreateArrowSprite("", location, Direction);
            _Entity.projectileSprite = newSprite;
            // location = game.position;
            // Direction = game.CurrentDirection;
            // newSprite = WeaponFactory.CreateArrowSprite("", location, 3, Direction);
            // game.Weapon = newSprite;
        }

    }
}

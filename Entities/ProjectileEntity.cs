﻿using System;
namespace SprintZero1.Entities
{
	public class ProjectileEntity : IEntity
    {
        private Direction _projectileDirection;
        private Vector2 _projectilePosition;
        private readonly WeaponSpriteFactory _WeaponSpriteFactory = WeaponSpriteFactory.Instance;
        public Vector2 Position { get { return _projectilePosition; } set { _projectilePosition = value; } }
        private ISprite projectileSprite;

        public ProjectileEntity(Vector2 position,Direction facingDirection)
		{
            _projectileDirection = facingDirection;
            _projectilePosition = position;
            projectileSprite = null;

        }

        public void Update(GameTime gameTime)
        {
            projectileSprite?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (projectileSprite != null)
            {
                projectileSprite.Draw(spriteBatch, _projectilePosition, spriteEffects);
            }
        }
    }
}
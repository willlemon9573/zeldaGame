using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.projectile;
using SprintZero1.Sprites;
using System.Runtime.CompilerServices;

namespace SprintZero1.Entities
{
	internal class ProjectileEntity : IEntity
    {
        private Direction _projectileDirection;
        private Vector2 _projectilePosition;
        private readonly WeaponSpriteFactory _WeaponSpriteFactory = WeaponSpriteFactory.Instance;
        public Vector2 Position { get { return _projectilePosition; } set { _projectilePosition = value; } }
        public Direction Direction { get { return _projectileDirection; } set { _projectileDirection = value; } }
        public ISprite projectileSprite;
        public IProjectile projectileUpdate;

        public ProjectileEntity(Vector2 position,Direction facingDirection)
		{
            _projectileDirection = facingDirection;
            _projectilePosition = position;
            projectileSprite = null;
            projectileUpdate= null;

        }

        public void Update(GameTime gameTime)
        {
<<<<<<< HEAD
            if (projectileSprite != null)
            {
                projectileSprite.Update(gameTime);
                projectileUpdate.Update(gameTime);
            }
=======
            projectileSprite?.Update(gameTime);
>>>>>>> 8681126206c6d2bb925a1e47157ad1834ba4b20a
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectileSprite != null)
            {
<<<<<<< HEAD
=======
                SpriteEffects spriteEffects = SpriteEffects.None;
>>>>>>> 8681126206c6d2bb925a1e47157ad1834ba4b20a
                projectileSprite.Draw(spriteBatch, _projectilePosition, spriteEffects);
            }
        }
    }
}
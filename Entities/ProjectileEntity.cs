using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
namespace SprintZero1.Entities
{
	public class ProjectileEntity : IEntity
    {
        private Direction _projectileDirection;
        private Vector2 _projectilePosition;
        private readonly WeaponSpriteFactory _WeaponSpriteFactory = WeaponSpriteFactory.Instance;
        public Vector2 Position { get { return _projectilePosition; } set { _projectilePosition = value; } }
        public Direction Direction { get { return _projectileDirection; } set { _projectileDirection = value; } }
        public ISprite projectileSprite;

        public ProjectileEntity(Vector2 position,Direction facingDirection)
		{
            _projectileDirection = facingDirection;
            _projectilePosition = position;
            projectileSprite = null;

        }

        public void Update(GameTime gameTime)
        {
            //projectileSprite?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (projectileSprite != null)
            {
                //projectileSprite.Draw(spriteBatch, _projectilePosition, spriteEffects);
            }
        }
    }
}
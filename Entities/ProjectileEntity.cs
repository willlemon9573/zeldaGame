using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.projectile;
using SprintZero1.Sprites;
using System;
using System.Diagnostics;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Represents a projectile entity. This class is designed with deferred property initialization in mind.
    /// Properties are expected to be set externally before the first use in game logic.
    /// </summary>
    internal class ProjectileEntity : IProjectileEntity
    {
        private Direction _projectileDirection;
        private Vector2 _projectilePosition;
        public Vector2 Position { get { return _projectilePosition; } set { _projectilePosition = value; } }
        public Direction Direction { get { return _projectileDirection; } set { _projectileDirection = value; } }
        private ISprite projectileSprite;
        private IProjectile projectileUpdate;
        private ISprite endingSprite;
        private SpriteEffects _SpriteEffects;
        private float _rotation;
        public float Rotation { get { return _rotation; } set { _rotation = value; } }
        public SpriteEffects _ChangeSpriteEffects { get { return _SpriteEffects; } set { _SpriteEffects = value; } }
        public ISprite ProjectileSprite
        {
            get { return projectileSprite; }
            set { projectileSprite = value; }
        }

        public IProjectile ProjectileUpdate
        {
            get { return projectileUpdate; }
            set { projectileUpdate = value; }
        }

        public ISprite EndingSprite
        {
            get { return endingSprite; }
            set { endingSprite = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileEntity"/> class.
        /// Note: Property values should be set externally before using this instance in game logic.
        /// </summary>
        public ProjectileEntity()
        {
            // No initialization code needed
        }

        /// <summary>
        /// Updates the projectile's state over time.
        /// </summary>
        /// <param name="gameTime">The game time information.</param>
        public void Update(GameTime gameTime)
        {
            projectileSprite?.Update(gameTime);
            projectileUpdate?.Update(gameTime);
            BackgroundSpriteEntity a = new BackgroundSpriteEntity(null, new Vector2());
        }

        /// <summary>
        /// Draws the projectile on the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (projectileSprite == null)
            {
                return;
            }
            projectileSprite.Draw(spriteBatch, _projectilePosition, _SpriteEffects, _rotation);
        }

        ~ProjectileEntity()
        {

        }
    }
}

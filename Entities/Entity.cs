using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal abstract class Entity : IEntity
    {
        public ISprite _sprite;
        public Vector2 _position;

        public Vector2 Position { get { return _position; } set { _position = value; } }

        public Entity(ISprite sprite, Vector2 position)
        {
            this._sprite = sprite;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            // Update Logic 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position, SpriteEffects.None, 0f);
        }
    }
}

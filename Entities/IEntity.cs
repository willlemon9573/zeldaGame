using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Entities
{
    internal interface IEntity
    {
        public Vector2 Position { get; set; }
        public void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch);
    }
}

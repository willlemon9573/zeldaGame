using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Entities
{
    /// <summary>
    /// Using for testing inventory
    /// </summary>
    internal class InventoryEntityTest : IEntity
    {
        // not going to have this be drawn anyways so useless
        private Vector2 _position = new Vector2(0, 0);
        public Vector2 Position { get { return _position; } set { _position = value; } }

        // not using a constructor

        public void Draw(SpriteBatch spriteBatch)
        {
            // not used
        }

        public void Update(GameTime gameTime)
        {
            // not used
        }
    }
}

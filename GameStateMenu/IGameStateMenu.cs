using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
    public interface IGameStateMenu
    {
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics);

        public void Update(GameTime gameTime);
    }
}
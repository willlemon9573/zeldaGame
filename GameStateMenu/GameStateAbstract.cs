using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
	public abstract class GameStateAbstract : IGameStateMenu
    {
        protected SpriteFont _font;
        protected Texture2D _overlay;
		protected string pauseText;


        public GameStateAbstract(SpriteFont font, GraphicsDevice graphicsDevice)
		{
			_font = font;
			_overlay = new Texture2D(graphicsDevice, 1, 1);
		}

		public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
		{
			spriteBatch.Draw(_overlay, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
			Vector2 textSize = _font.MeasureString(pauseText);
			Vector2 textPosition = new Vector2((graphics.PreferredBackBufferWidth - textSize.X) / 2, (graphics.PreferredBackBufferHeight - textSize.Y) / 2);
			spriteBatch.DrawString(_font, pauseText, textPosition, Color.White);
;
		}
	}
}
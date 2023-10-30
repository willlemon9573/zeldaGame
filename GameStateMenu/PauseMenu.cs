using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
	public class PauseMenu
	{
		private SpriteFont _font;
		private Texture2D _overlay;
		private bool _isVisible;

		public PauseMenu(SpriteFont font, GraphicsDevice graphicsDevice)
		{
			_font = font;
			_overlay = new Texture2D(graphicsDevice, 1, 1);
			_overlay.SetData(new[] { new Color(0, 0, 0, 150) });
			_isVisible = false;
		}

		public void Show()
		{
			_isVisible = true;
		}

		public void Hide()
		{
			_isVisible = false;
		}

		public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
		{
			if (!_isVisible) return;

			spriteBatch.Draw(_overlay, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);

			string pauseText = "Pause";
			Vector2 textSize = _font.MeasureString(pauseText);
			Vector2 textPosition = new Vector2((graphics.PreferredBackBufferWidth - textSize.X) / 2, (graphics.PreferredBackBufferHeight - textSize.Y) / 2);
			spriteBatch.DrawString(_font, pauseText, textPosition, Color.White);
;
		}
	}
}
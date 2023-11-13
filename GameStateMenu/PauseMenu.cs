using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
    /// <summary>
    /// Represents the pause menu in the game.
    /// This class extends GameStateAbstract and manages the display and functionality of the pause menu.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class PauseMenu : GameStateAbstract
    {
        private const int RGB_BLACK = 0;
        private const int RGB_ALPHA = 225;
        private readonly string pauseText;

        public PauseMenu(Game1 game) : base(game)
        {
            // Load the font used for displaying pause text
            _font = game.Content.Load<SpriteFont>("PauseSetting");
            // Set the text to be displayed during pause
            pauseText = "Pause";
            // Create a gray overlay color to indicate paused state
            Color grayOverlay = new Color(RGB_BLACK, RGB_BLACK, RGB_BLACK, RGB_ALPHA);
            // Apply the overlay color
            _overlay.SetData(new[] { grayOverlay });
            // Initialize the command to unpause the game
            // Initialize the list to keep track of previously pressed keys

        }

        public override void Update(GameTime gameTime)
        {
            //no implementation
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            Vector2 textSize = _font.MeasureString(pauseText);
            Vector2 textPosition = new Vector2((WIDTH - textSize.X) / 2, (HEIGHT - textSize.Y) / 2);
            spriteBatch.DrawString(_font, pauseText, textPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

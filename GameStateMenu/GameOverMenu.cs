using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.GameStateMenu
{
    /// <summary>
    /// Represents the Game Over menu in the game.
    /// This class extends GameStateAbstract and handles the rendering and updating of the game over state.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class GameOverMenu : GameStateAbstract
    {
        private ICommand _unPauseGame; // Command to unpause the game
        private string gameOverText; // Text to display on game over

        private const int RGB_BLACK = 0; // Constant for black color in RGB
        private const int RGB_ALPHA = 225; // Alpha value for overlay
        private Color redOverlay; // Red color overlay for the game over screen
        private List<Keys> _previouslyPressedKeys; // Stores previously pressed keys

        public GameOverMenu(Game1 game) : base(game)
        {
            _previouslyPressedKeys = new List<Keys> { Keys.Escape };
            _font = game.Content.Load<SpriteFont>("DeathmatchFont");
            gameOverText = "YOU DIED";
            redOverlay = new Color(150, 0, 0, 150); // Red color with some transparency

            _overlay.SetData(new[] { redOverlay }); // Set the overlay data
            _unPauseGame = new UnpauseGameCommand(); // Initialize unpause command
        }

        public override void Update(GameTime gameTime)
        {
            // Get the current state of the keyboard
            KeyboardState currentKeyboardState = Keyboard.GetState();
            // Get the list of currently pressed keys
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();

            // Iterate through the pressed keys
            foreach (Keys key in pressedKeys)
            {

                // Check if 'Escape' key is pressed and was not pressed in the previous frame
                if (key == Keys.Escape && !_previouslyPressedKeys.Contains(key))
                {
                    // Execute the unpause command
                    _unPauseGame.Execute();
                }
            }

            // Update the list of previously pressed keys
            _previouslyPressedKeys = pressedKeys.ToList();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the overlay and game over text
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);
            Vector2 textSize = _font.MeasureString(gameOverText);
            Vector2 textPosition = new Vector2((WIDTH - textSize.X) / 2, (HEIGHT - textSize.Y) / 2);

            spriteBatch.DrawString(_font, gameOverText, textPosition, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);
        }



    }

}

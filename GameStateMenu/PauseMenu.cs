using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SprintZero1.GameStateMenu
{
    public class PauseMenu : GameStateAbstract
    {
        private readonly ICommand _unPauseGame;
        private const int RGB_BLACK = 0;
        private const int RGB_ALPHA = 225;
        private readonly string pauseText;
        private List<Keys> _previouslyPressedKeys;

        public PauseMenu(Game1 game) : base(game)
        {
            _previouslyPressedKeys = new List<Keys> { Keys.Escape };
            // Load the font used for displaying pause text
            _font = game.Content.Load<SpriteFont>("PauseSetting");
            // Set the text to be displayed during pause
            pauseText = "Pause";
            // Create a gray overlay color to indicate paused state
            Color grayOverlay = new Color(RGB_BLACK, RGB_BLACK, RGB_BLACK, RGB_ALPHA);
            // Apply the overlay color
            _overlay.SetData(new[] { grayOverlay });
            // Initialize the command to unpause the game
            _unPauseGame = new UnpauseCommand((BaseGameState)game.GameState);
            // Initialize the list to keep track of previously pressed keys

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
                // For debugging purposes, log the key and its previous state
                Debug.WriteLine(key.ToString());
                Debug.WriteLine(_previouslyPressedKeys.Contains(key));

                // Check if 'Escape' key is pressed and was not pressed in the previous frame
                if (key == Keys.Escape && !_previouslyPressedKeys.Contains(key))
                {
                    // Execute the unpause command
                    _unPauseGame.Execute();
                    // Log the execution of unpause for debugging
                    Debug.WriteLine("Unpause executed");
                }
            }

            // Update the list of previously pressed keys
            _previouslyPressedKeys = pressedKeys.ToList();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);

            Vector2 textSize = _font.MeasureString(pauseText);
            Vector2 textPosition = new Vector2((WIDTH - textSize.X) / 2, (HEIGHT - textSize.Y) / 2);
            spriteBatch.DrawString(_font, pauseText, textPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);



        }
    }
}

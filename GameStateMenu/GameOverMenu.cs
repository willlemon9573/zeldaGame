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
    public class GameOverMenu : GameStateAbstract
    {
        private ICommand _unPauseGame;
        private string gameOverText;

        private const int RGB_BLACK = 0;
        private const int RGB_ALPHA = 225;
        private Color redOverlay;
        private List<Keys> _previouslyPressedKeys;

        public GameOverMenu(Game1 game) : base(game)
        {
            _previouslyPressedKeys = new List<Keys> { Keys.Escape };
            _font = game.Content.Load<SpriteFont>("DeathmatchFont");
            gameOverText = "YOU DIED";
            redOverlay = new Color(150, 0, 0, 150);

            _overlay.SetData(new[] { redOverlay });
            _unPauseGame = new UnpauseCommand((BaseGameState)game.GameState);
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
            Vector2 textSize = _font.MeasureString(gameOverText);
            Vector2 textPosition = new Vector2((WIDTH - textSize.X) / 2, (HEIGHT - textSize.Y) / 2);

            spriteBatch.DrawString(_font, gameOverText, textPosition, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);
        }



    }
    
}

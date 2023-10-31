using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.GameStateMenu
{
    public class PauseMenu : GameStateAbstract
    {
        private ICommand _unPauseGame;

        public PauseMenu(Game1 game) : base(game)
        {
            _font = game.Content.Load<SpriteFont>("PauseSetting");
            pauseText = "Pause";
            _overlay.SetData(new[] { new Color(0, 0, 0, 225) }); //gray overlay
            _unPauseGame = new UnpauseCommand((BaseGameState)game.GameState);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                Debug.WriteLine(key.ToString());
                Debug.WriteLine(_previouslyPressedKeys.Contains(key));
                if (key == Keys.U && !_previouslyPressedKeys.Contains(key))
                {
                    _unPauseGame.Execute();
                    Debug.WriteLine("uppause executed");
                }
            }
            _previouslyPressedKeys = pressedKeys.ToList<Keys>();
        }
    }
}
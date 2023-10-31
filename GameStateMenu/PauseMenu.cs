using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.StatePatterns.GameStatePatterns;

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
                if (key == Keys.Escape)
                {
                    _unPauseGame.Execute();
                }
            }
        }
    }
}
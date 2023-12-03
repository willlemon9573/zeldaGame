using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Enums;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GamePausedState : BaseGameState
    {
        private const float BufferTime = 1 / 7f; // to prevent the game from unpausing after player presses escape
        private float _elapsedTime;
        private IGameStateMenu pauseGame;
        private readonly Keys unpauseKey = Keys.Escape;
        private List<Keys> previouslyPressedKeys;

        /// <summary>
        /// Handles unpausing the game
        /// </summary>
        /// <param name="pressedKeys">List of currently pressed keys</param>
        private void CheckInput(List<Keys> pressedKeys)
        {

            if (previouslyPressedKeys.Contains(unpauseKey) && !pressedKeys.Contains(unpauseKey))
            {
                GameStatesManager.ChangeGameState(GameState.Playing);
                previouslyPressedKeys.Clear();
            }

            previouslyPressedKeys = pressedKeys;
        }

        public GamePausedState(Game1 game) : base(game)
        {
            pauseGame = new PauseMenu(game);
            previouslyPressedKeys = new List<Keys>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pauseGame.Draw(spriteBatch);
            GameStatesManager.GetGameState(GameState.Playing).Draw(spriteBatch);
        }

        public override void Handle()
        {
            _elapsedTime = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            List<Keys> pressedKeys = Keyboard.GetState().GetPressedKeys().ToList();
            /* buffer meant to prevent the game from unpausing after the player presses the pause button */
            if (_elapsedTime >= BufferTime)
            {
                pauseGame.Update(gameTime);
                CheckInput(pressedKeys);
            }
            else if (pressedKeys.Contains(unpauseKey) == false) // to prevent player from holding key for too long and accidentally unpausing
            {
                _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}

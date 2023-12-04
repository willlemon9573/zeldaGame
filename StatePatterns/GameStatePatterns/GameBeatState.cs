using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Commands.PlayerCommands;
using SprintZero1.Enums;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System.Linq;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameBeatState : BaseGameState
    {
        private IGameStateMenu endingState;
        private ICommand _resetCommand;
        private IGameState playingState;
        public GameBeatState(Game1 game) : base(game)
        {
            endingState = new GameBeatMenu(game);
            _resetCommand = new ResetGameCommand();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            playingState.Draw(spriteBatch);
            // Link should still be drawn
            endingState.Draw(spriteBatch);
        }

        public override void Handle()
        {
            playingState = GameStatesManager.GetGameState(GameState.Playing);
        }

        public override void Update(GameTime gameTime)
        {

            endingState.Update(gameTime);
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Contains(Keys.R))
            {
                _resetCommand.Execute();
            }
        }
    }
}

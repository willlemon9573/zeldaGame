using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

using SprintZero1.Controllers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    public class GamePausedState : BaseGameState
    {
        private ItemSelectionMenu itemSelectionMenu;
        private IGameStateMenu pauseGame;
        private IController controllerForItemSelection;
        public GamePausedState(Game1 game) : base(game)
        {
            itemSelectionMenu = new ItemSelectionMenu(game, ProgramManager.player);
            pauseGame = new PauseMenu(game);
            controllerForItemSelection = new KeyboardControllerForItemSelection(game, ProgramManager.player, itemSelectionMenu);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ProgramManager.Draw(spriteBatch);
            pauseGame.Draw(spriteBatch);
        }

        public override void Handle()
        {

        }

        public override void Update(GameTime gameTime)
        {
            pauseGame.Update(gameTime);
            controllerForItemSelection.Update();
        }
    }
}

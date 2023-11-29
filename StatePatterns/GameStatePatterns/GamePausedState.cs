using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GamePausedState : BaseGameState
    {
        private ItemSelectionMenu itemSelectionMenu;
        private IGameStateMenu pauseGame;
        private IController controllerForItemSelection;
        private Game1 game;
        public GamePausedState(Game1 game) : base(game)
        {
            this.game = game;
            /*itemSelectionMenu = new ItemSelectionMenu(game, player);
            pauseGame = new PauseMenu(game);
            controllerForItemSelection = new KeyboardControllerForItemSelection(game, player, itemSelectionMenu);*/
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pauseGame.Draw(spriteBatch);
            GameStatesManager.GetGameState(Enums.GameState.Playing).Draw(spriteBatch);
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

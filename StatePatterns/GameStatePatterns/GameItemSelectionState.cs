using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;
using SprintZero1.Controllers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameItemSelectionState : BaseGameState
    {
        private ItemSelectionMenu itemSelectionMenu;
        private IController controllerForItemSelection;
        public GameItemSelectionState(Game1 game) : base(game)
        {
            itemSelectionMenu = new ItemSelectionMenu(game, ProgramManager.Player);
            controllerForItemSelection = new KeyboardControllerForItemSelection(game, ProgramManager.Player, itemSelectionMenu);
        }

        public override void Update(GameTime gameTime)
        {
            controllerForItemSelection.Update();
            itemSelectionMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            itemSelectionMenu.Draw(spriteBatch);
        }

        public override void Handle()
        {
            (itemSelectionMenu as ItemSelectionMenu).SynchronizeInventory();
        }

    }
}

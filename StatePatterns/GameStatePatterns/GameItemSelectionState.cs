using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameItemSelectionState : BaseGameState
    {
        private IGameStateMenu itemSelectionMenu;
        public GameItemSelectionState(Game1 game) : base(game)
        {
            itemSelectionMenu = new ItemSelectionMenu(game, ProgramManager.Player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Handle()
        {
            (itemSelectionMenu as ItemSelectionMenu).SynchronizeInventory();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace SprintZero1.GameStateMenu
{
	public class PauseMenu : GameStateAbstract
    {


        public PauseMenu(Game1 game): base(game)
		{
            _font = game.Content.Load<SpriteFont>("PauseSetting");
            pauseText = "Pause";
            _overlay.SetData(new[] { new Color(0, 0, 0, 225) }); //gray overlay

        }

        public override void Update(GameTime gameTime)
        {
            //no implementation
        }
    }
}
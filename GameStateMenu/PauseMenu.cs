using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
	public class PauseMenu : GameStateAbstract
    {


        public PauseMenu(SpriteFont font, GraphicsDevice graphicsDevice): base(font, graphicsDevice)
		{
            pauseText = "Pause";

        }

        public override void Update(GameTime gameTime)
        {
            //no implementation
        }
    }
}
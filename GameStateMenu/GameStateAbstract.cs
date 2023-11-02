using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.StatePatterns.GameStatePatterns;
using System.Diagnostics;
using System.Collections.Generic;


namespace SprintZero1.GameStateMenu
{
    public abstract class GameStateAbstract : IGameStateMenu
    {
        protected SpriteFont _font;
        protected Texture2D _overlay;
        protected GraphicsDeviceManager graphics;
        protected const int WIDTH = 256;
        protected const int HEIGHT = 256;

        public GameStateAbstract(Game1 game)
        {
            graphics = game.Graphics;
            _overlay = new Texture2D(game.GraphicsDevice, 1, 1);
        }

        public abstract void Update(GameTime gameTime);


        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
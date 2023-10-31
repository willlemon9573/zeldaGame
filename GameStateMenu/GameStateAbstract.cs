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
        protected string pauseText;
        protected GraphicsDeviceManager graphics;
        protected const int WIDTH = 256;
        protected const int HEIGHT = 256;
        protected List<Keys> _previouslyPressedKeys;

        public GameStateAbstract(Game1 game)
        {
            _previouslyPressedKeys = new List<Keys>();
            graphics = game.Graphics;
            _overlay = new Texture2D(game.GraphicsDevice, 1, 1);
        }

        public abstract void Update(GameTime gameTime);
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);

            Vector2 textSize = _font.MeasureString(pauseText);
            Vector2 textPosition = new Vector2((WIDTH - textSize.X) / 2, (HEIGHT - textSize.Y) / 2);
            spriteBatch.DrawString(_font, pauseText, textPosition, Color.White);



        }
    }
}
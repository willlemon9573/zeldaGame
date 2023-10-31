using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace SprintZero1.GameStateMenu
{
    public abstract class GameStateAbstract : IGameStateMenu
    {
        protected SpriteFont _font;
        protected Texture2D _overlay;
        protected string pauseText;
        protected GraphicsDeviceManager graphics;


        public GameStateAbstract(Game1 game)
        {
            graphics = game.Graphics;
            _overlay = new Texture2D(game.GraphicsDevice, 1, 1);
        }

        public abstract void Update(GameTime gameTime);
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, 256, 240), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);

            Vector2 textSize = _font.MeasureString(pauseText);
            Debug.WriteLine(textSize);
            Debug.WriteLine((graphics.PreferredBackBufferWidth - textSize.X) / 2);
            Debug.WriteLine((graphics.PreferredBackBufferHeight - textSize.Y) / 2);
            Vector2 textPosition = new Vector2((256 - textSize.X) / 2, (240 - textSize.Y) / 2);
            //(graphics.PreferredBackBufferWidth - textSize.X) / 2, (graphics.PreferredBackBufferHeight - textSize.Y) / 2
            spriteBatch.DrawString(_font, pauseText, textPosition, Color.White);

            //spriteBatch.DrawString(_font, pauseText, textPosition, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, 0f);


        }
    }
}
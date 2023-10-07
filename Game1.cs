using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.src;
using System;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /* Variables for window rescaling */
        private const int WINDOW_SCALE = 4;
        private RenderTarget2D _newRenderTarget;
        private Rectangle _actualScreenRectangle;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Code for Window rescaling
            _graphics.PreferredBackBufferWidth = 256 * WINDOW_SCALE;
            _graphics.PreferredBackBufferHeight = 240 * WINDOW_SCALE;
            this.IsFixedTimeStep = true;//false;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
        }

        /// <summary>
        /// Initialize all components required to run the game
        /// </summary>
        protected override void Initialize()
        {
            // code for window rescaling
            _newRenderTarget = new RenderTarget2D(GraphicsDevice, 256, 240);
            _actualScreenRectangle = new Rectangle(0, 0, 256 * WINDOW_SCALE, 240 * WINDOW_SCALE);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            ProgramManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            ProgramManager.Draw(_spriteBatch);
            _spriteBatch.End();

            // Code for rescaling
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_newRenderTarget, _actualScreenRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
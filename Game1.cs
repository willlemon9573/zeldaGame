using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
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
            controller = new KeyboardController();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2DManager.LoadAllTextures(this.Content);
            LinkSpriteFactory.Instance.LoadTextures();
            TileSpriteFactory.Instance.LoadTextures();
            TestingManager.StartTest(this);
            TestingManager.TestPlayerEntityWithKeyboard(new Vector2(176, 170), 1, Direction.South);
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(1), new Vector2(200, 100));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(2), new Vector2(55, 100));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(3), new Vector2(55, 205));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(4), new Vector2(200, 205));
        }

        protected override void Update(GameTime gameTime)
        {
            ProgramManager.Update(gameTime);
            TestingManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(this._newRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            ProgramManager.Draw(_spriteBatch);
            TestingManager.Draw(_spriteBatch);
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
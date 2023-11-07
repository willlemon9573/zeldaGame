using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private MouseController _mouseController;
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

        /// Initialize all components required to run the game
        /// </summary>
        protected override void Initialize()
        {
            // code for window rescaling
            _newRenderTarget = new RenderTarget2D(GraphicsDevice, 255, 240);
            _actualScreenRectangle = new Rectangle(0, 0, 255 * WINDOW_SCALE, 240 * WINDOW_SCALE);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2DManager.LoadAllTextures(this.Content);
            Texture2DManager.LoadSpriteFonts(this.Content);
            /* 
             * Factories are missing a lot of comments. To be added in Sprint 4 
             * May also be loading textures specifically Program Manager rather than in game1.cs
             */
            EnemySpriteFactory.Instance.LoadTextures();
            LinkSpriteFactory.Instance.LoadTextures();
            TileSpriteFactory.Instance.LoadTextures();
            WeaponSpriteFactory.Instance.LoadTextures();
            ItemSpriteFactory.Instance.LoadTextures();
            _mouseController = new MouseController(this);
            ItemSpriteFactory.Instance.LoadTextures();
            HUDManager.Initialize();
            /*ProgramManager.Start(this);*/
            LevelManager.Initialize(this);

        }

        protected override void Update(GameTime gameTime)
        {
            LevelManager.Update(gameTime);
            _mouseController.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            /* Draw all sprites on a new render target */
            GraphicsDevice.SetRenderTarget(this._newRenderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);
            LevelManager.Draw(_spriteBatch);
            HUDManager.Draw(_spriteBatch);
            _spriteBatch.End();

            /* Rescale the window and draw sprite batch with new scale */
            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_newRenderTarget, _actualScreenRectangle, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
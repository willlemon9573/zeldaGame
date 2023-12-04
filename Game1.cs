using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System;

namespace SprintZero1
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        /* Variables for window rescaling */
        private const int WINDOW_SCALE = 4;
        private RenderTarget2D _newRenderTarget;
        private Rectangle _actualScreenRectangle;
        public Nullable<Matrix> Translation = null;
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

        public void Reset()
        {
            GameStatesManager.InitializeGameStateMap(this);
            LevelManager.Load();
            GameStatesManager.Start();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadTextures();
            SoundFactory.StartSoundFactory(this.Content);
            GameStatesManager.InitializeGameStateMap(this);
            LevelManager.Load();
            GameStatesManager.Start();
        }

        private void LoadTextures()
        {
            Texture2DManager.LoadAllTextures(this.Content);
            Texture2DManager.LoadSpriteFonts(this.Content);
            EnemySpriteFactory.Instance.LoadTextures();
            PlayerSpriteFactory.Instance.LoadTextures();
            TileSpriteFactory.Instance.LoadTextures();
            WeaponSpriteFactory.Instance.LoadTextures();
            ItemSpriteFactory.Instance.LoadTextures();
            HUDSpriteFactory.Instance.LoadTextures();
            ItemSpriteFactory.Instance.LoadTextures();
        }

        protected override void Update(GameTime gameTime)
        {
            GameStatesManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            /* Draw all sprites on a new render target */
            GraphicsDevice.SetRenderTarget(this._newRenderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, Translation);
            GameStatesManager.Draw(_spriteBatch);
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
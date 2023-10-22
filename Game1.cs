using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System;
using System.Collections.Generic;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private MouseController _mouseController;
        private SpriteBatch _spriteBatch;
        private LevelManager levelManager;
        /* Variables for window rescaling */
        private const int WINDOW_SCALE = 4;
        private RenderTarget2D _newRenderTarget;
        private Rectangle _actualScreenRectangle;
        List<String> levelList = new List<String> { "XMLFiles/Room1.xml", "XMLFiles/Room1Left.xml", "XMLFiles/Room1Right.xml", "XMLFiles/Room2.xml",
        "XMLFiles/Room3.xml", "XMLFiles/Room3Left.xml", "XMLFiles/Room3Right.xml", "XMLFiles/Room4.xml", "XMLFiles/Room4Left.xml", "XMLFiles/Room4Leftest.xml",
                "XMLFiles/Room4Right.xml", "XMLFiles/Room4Rightest.xml", "XMLFiles/Room5.xml", "XMLFiles/Room5Right.xml", "XMLFiles/Room5Rightest.xml",
                "XMLFiles/Room6.xml","XMLFiles/Room6Left.xml","XMLFiles/RoomSecret.xml" , "XMLFiles/RoomDev.xml"};
        private int levelListIndex;

        public List<string> LevelList 
         { 
             get { return levelList; } 
         }

         public int LevelListIndex
         {
            get { return levelListIndex; }
            set { levelListIndex = value; }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Code for Window rescaling
            _graphics.PreferredBackBufferWidth = 255 * WINDOW_SCALE;
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
            _newRenderTarget = new RenderTarget2D(GraphicsDevice, 255, 240);
            _actualScreenRectangle = new Rectangle(0, 0, 255 * WINDOW_SCALE, 240 * WINDOW_SCALE);
            levelManager = new LevelManager(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2DManager.LoadAllTextures(this.Content);
            TileSpriteFactory.Instance.LoadTextures();
            LinkSpriteFactory.Instance.LoadTextures();
            _mouseController = new MouseController(this);
            
            
            levelManager.Initialize();
            /* FOR TESTING */
            TestingManager.StartTest(this);
            /* doors need to be drawn BEFORE walls because doors overlap them to make sure the "bricks" at the top match" */
            /*TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateFloorSprite("entrance"), new Vector2(127, 151));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(1), new Vector2(199, 100));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(2), new Vector2(55, 100));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(3), new Vector2(55, 204));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewWallSprite(4), new Vector2(199, 204));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewTileSprite("open_north"), new Vector2(127, 80));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewTileSprite("open_west"), new Vector2(15, 152));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewTileSprite("open_east"), new Vector2(239, 152));
            TestingManager.AddStaticSprite(TileSpriteFactory.Instance.CreateNewTileSprite("open_south"), new Vector2(127, 224));
           
            TestingManager.TestPlayerEntityWithKeyboard(new Vector2(176, 170), 1, Direction.South); */
        }

        protected override void Update(GameTime gameTime)
        {
            levelManager.Update(gameTime);
            //TestingManager.Update(gameTime);
            _mouseController.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(this._newRenderTarget);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            levelManager.Draw(_spriteBatch);
            //TestingManager.Draw(_spriteBatch);

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
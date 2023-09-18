/* 
 * Sprint zero game project
 * Aaron Heishman
 * CSE 3902 - Proj: Interact Sys
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace SprintZero1
{   
    public class Game1 : Game
    {   
        /* Commented out code has been deprecated */
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController, mouseController;
        private IBlockFactory blockFactory;
        private ISprite nonMovingBlock;
        private int onScreenBlockIndex;

        public int OnScreenBlockPos
        {
            get { return onScreenBlockIndex; }
            set { onScreenBlockIndex = value; }
        }
        public ISprite NonMovingBlock
        {
            set { nonMovingBlock = value; }
        }
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initialize all components required to run the game
        /// </summary>
        protected override void Initialize()
        {
            keyboardController = new KeyboardController();
            // mouseController = new MouseController();
            // credits = new CreditsSprite();
            //soraSprite = new StandingInPlaceSora();
            keyboardController.LoadDefaultCommands(this);
            // mouseController.LoadDefaultCommands(this);
            blockFactory = BlockFactory.Instance;
            blockFactory.Initialize();
            OnScreenBlockPos = 0;
            base.Initialize();
        }

        /// <summary>
        /// Loads the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // spriteSheet = this.Content.Load<Texture2D>("SoraSprites1");
            blockFactory.LoadTextures(this.Content);
            nonMovingBlock = blockFactory.CreateNonMovingBlockSprite("flat"); // default block shown is flat
        }

        /// <summary>
        /// Updates game logic
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            // mouseController.Update();
            // soraSprite.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws game objects
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // credits.Draw(_spriteBatch, spriteSheet);
            // soraSprite.Draw(_spriteBatch, spriteSheet);
            nonMovingBlock.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
/* 
 * Sprint zero game project
 * Aaron Heishman
 * CSE 3902 - Proj: Interact Sys
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.Commands;



namespace SprintZero1
{
    public class Game1 : Game
    {
        /* Commented out code has been deprecated */
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController;
        private IBlockFactory blockFactory;
        private ISprite nonMovingOnScreenBlock;
        private int onScreenBlockIndex;
        public int OnScreenBlockIndex
        {
            get { return onScreenBlockIndex; }
            set { onScreenBlockIndex = value; }
        }
        public ISprite NonMovingBlock
        {
            set { nonMovingOnScreenBlock = value; }
        }
        
        //ALL the variables needs for create a LinkSprite
        public ISprite Link { get; set; }
        internal ILinkFactory linkFactory { get; set; }
        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
            set { _position = value; }
        }
        public int CurrentDirection { get; set; }
        //0 is facingUp. 1 is facingDown. 2 is facingLeft. 3 is facingRight
        public int CurrentFrame { get; set; }
        public bool isAttacking { get; set; }


        public void SetLink(ISprite newLink)
        {
            Link = newLink;
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
            _position = new Vector2(100, 100);
            blockFactory = BlockFactory.Instance;
            OnScreenBlockIndex = 0;

            linkFactory = new LinkFactory();
            CurrentDirection = 1;
            CurrentFrame = 0;
            isAttacking = false;


            keyboardController = new KeyboardController();
            keyboardController.LoadDefaultCommands(this);
            base.Initialize();
        }

        /// <summary>
        /// Loads the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blockFactory.LoadTextures(this.Content);
            nonMovingOnScreenBlock = blockFactory.CreateNonMovingBlockSprite("flat"); // default block shown is flat
            linkFactory.LoadTextures(this.Content);
            Link = linkFactory.createNewLink(CurrentDirection, position, CurrentFrame, isAttacking);

        }

        /// <summary>
        /// Updates game logic
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            Link.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws game objects
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            nonMovingOnScreenBlock.Draw(_spriteBatch);
            Link.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
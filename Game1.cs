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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        /* Temporary assignment of code until managers are made */
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

        private IUsableItemFactory itemFactory;
        private ISprite onScreenItem;
        private int onScreenItemIndex;
        internal int direction;

        public int OnScreenItemIndex
        {
            get { return onScreenItemIndex; }
            set { onScreenItemIndex = value; }
        }

        public ISprite Item
        {
            set { onScreenItem = value; }
        }
        /* end of temporary assignment of code */
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
            CurrentDirection = 2;
            CurrentFrame = 0;
            isAttacking = false;
            keyboardController = new KeyboardController();
            keyboardController.LoadDefaultCommands(this);
            itemFactory = ItemFactory.Instance;
            keyboardController = new KeyboardController();
            keyboardController.LoadDefaultCommands(this);
            OnScreenBlockIndex = 0;
            OnScreenItemIndex = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blockFactory.LoadTextures(this.Content);
            linkFactory.LoadTextures(this.Content);
            Link = linkFactory.createNewLink(CurrentDirection, position, CurrentFrame, isAttacking);
            nonMovingOnScreenBlock = blockFactory.CreateNonMovingBlockSprite("flat", new Vector2(200, 230)); // default block shown is flat
            itemFactory.LoadTextures(this.Content);
            onScreenItem = itemFactory.CreateItemSprite("rubyStatic");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            Link.Update(gameTime);
            onScreenItem.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            nonMovingOnScreenBlock.Draw(_spriteBatch);
            Link.Draw(_spriteBatch);
            onScreenItem.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.Commands;
using SprintZero1.Level;
using SprintZero1.Players;
using SprintZero1.Characters;
using Microsoft.Xna.Framework.Input;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController;

        private IEnemyFactory enemyFactory;
        private ISprite enemyOnScreen;
        private int onScreenEnemyIndex;
        
        public int OnScreenEnemyIndex
        {
            get { return onScreenEnemyIndex; }
            set { onScreenEnemyIndex = value; }
        }

        public ISprite screenEnemy
        {
            set { enemyOnScreen = value;  }
        }

        public int CurrentFrame { get; set; }
        /* Temporary assignment of code until managers are made */
        private IBlockFactory blockFactory;
        private ISprite nonMovingOnScreenBlock;
        private int onScreenBlockIndex;
        public Enemy enemy;
        private LevelManager levelManager;
        
        

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
            enemyFactory = EnemyFactory.Instance;
            keyboardController = new KeyboardController();
            keyboardController.LoadDefaultCommands(this);
            OnScreenEnemyIndex = 0;
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
            enemyFactory.LoadTextures(this.Content);
            blockFactory.LoadTextures(this.Content);
            enemy = new Enemy(_spriteBatch, this.Content.Load<Texture2D>("Bosses"), this);
            enemy.EnemySprite = enemyFactory.CreateEnemySprite("dungeon_gel", new Vector2(600, 300), 0);
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
            enemy.Update(gameTime);
            onScreenItem.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            nonMovingOnScreenBlock.Draw(_spriteBatch);
            enemy.Draw();
            Link.Draw(_spriteBatch);
            onScreenItem.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
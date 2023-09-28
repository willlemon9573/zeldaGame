using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Factories;
using SprintZero1.Sprites;

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

        private IUsableItemFactory itemFactory;
        private ISprite onScreenItem;
        private int onScreenItemIndex;

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

        protected override void Initialize()
        {
            blockFactory = BlockFactory.Instance;
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
            nonMovingOnScreenBlock = blockFactory.CreateNonMovingBlockSprite("flat", new Vector2(200, 230)); // default block shown is flat
            itemFactory.LoadTextures(this.Content);
            onScreenItem = itemFactory.CreateItemSprite("rubyStatic");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            onScreenItem.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            nonMovingOnScreenBlock.Draw(_spriteBatch);
            onScreenItem.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
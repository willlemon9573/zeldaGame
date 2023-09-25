using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController;
        private ItemFactory itemFactory;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

       
        protected override void Initialize()
        {
            itemFactory = ItemFactory.Instance;
            keyboardController = new KeyboardController();
            keyboardController.LoadDefaultCommands(this);
            OnScreenItemIndex = 0;
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            onScreenItem.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
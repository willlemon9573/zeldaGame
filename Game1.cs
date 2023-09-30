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
        private ISprite weaponSpriteTest;
        private WeaponSpriteFactory weaponFactoryTest;

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
            weaponFactoryTest = WeaponSpriteFactory.Instance;
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
            itemFactory.LoadTextures(this.Content);
            weaponFactoryTest.LoadTextures(this.Content);
            nonMovingOnScreenBlock = blockFactory.CreateNonMovingBlockSprite("flat", new Vector2(200, 230)); // default block shown is flat
            onScreenItem = itemFactory.CreateItemSprite("rubyStatic");
            weaponSpriteTest = weaponFactoryTest.CreateBoomerangSprite("x", new Vector2(400, 240), 3, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            onScreenItem.Update(gameTime);
            weaponSpriteTest.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            nonMovingOnScreenBlock.Draw(_spriteBatch);
            onScreenItem.Draw(_spriteBatch);
            weaponSpriteTest.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
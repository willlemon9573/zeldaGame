/* 
 * Sprint zero game project
 * Aaron Heishman
 * CSE 3902 - Proj: Interact Sys
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Sprites;

namespace SprintZero1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController, mouseController;
        private Texture2D spriteSheet;
        
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
            mouseController = new MouseController();
            keyboardController.LoadDefaultCommands(this);
            mouseController.LoadDefaultCommands(this);
            base.Initialize();
        }

        /// <summary>
        /// Loads the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Updates game logic
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            mouseController.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws game objects
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the current sprite to the new sprite
        /// </summary>
        /// <param name="sprite">the new sprite to be displayed on screen</param>
        public void SetSprite(ISprite sprite)
        {
        }
    }
}
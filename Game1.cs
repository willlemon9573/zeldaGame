/* 
 * Sprint zero game project
 * Aaron Heishman
 * CSE 3902 - Proj: Interact Sys
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1
{   
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        
        
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
           
        }

        /// <summary>
        /// Loads the content for the game
        /// </summary>
        protected override void LoadContent()
        {
            
        }

        /// <summary>
        /// Updates game logic
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values</param>
        protected override void Update(GameTime gameTime)
        {
            
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
    }
}
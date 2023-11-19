using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{

    /// <summary>
    /// Abstract base class for different game state menus.
    /// This class provides common properties and methods used by various game states.
    /// </summary>
    /// <author>Zihe Wang</author>
    public abstract class GameStateAbstract : IGameStateMenu
    {
        protected SpriteFont _font; // Font for text rendering
        protected Texture2D _overlay; // Texture for rendering overlays
        protected GraphicsDeviceManager graphics; // Graphics device manager for handling graphics
        protected const int WIDTH = 255; // Width for the overlay and rendering
        protected const int HEIGHT = 240; // Height for the overlay and rendering
        protected GraphicsDevice _GraphicsDevice; // Graphics device for rendering

        /// <summary>
        /// Initializes a new instance of the GameStateAbstract class.
        /// </summary>
        /// <param name="game">Reference to the main game class for accessing graphics properties.</param>
        public GameStateAbstract(Game1 game)
        {
            graphics = game._graphics;
            _GraphicsDevice = game.GraphicsDevice;
            _overlay = new Texture2D(_GraphicsDevice, 1, 1); // Initialize overlay texture
        }

        /// <summary>
        /// Abstract method for updating the game state.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Abstract method for drawing the game state.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

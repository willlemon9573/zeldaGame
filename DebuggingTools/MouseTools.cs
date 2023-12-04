using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Managers;

namespace SprintZero1.DebuggingTools
{
    internal class MouseTools
    {
        private readonly int ScaleFactor = 4; /* change this value based on how much you're scaling the game */
        private MouseState _previousMouseState;
        private readonly Rectangle _windowRegion;
        private readonly SpriteFont _font;
        private Vector2 _currentMousePosition;
        private string _mousePositionText;
        private readonly Texture2D _pixels;
        private Vector2 _pixelPosition = Vector2.Zero;
        private Rectangle _positionRectangle = new Rectangle();
        private Color _positionRectangleColor = Color.White;

        /// <summary>
        /// Checks if the mouse is in the window or not
        /// </summary>
        /// <param name="mouseState"></param>
        /// <returns>true if the mouse is inside the game window</returns>
        private bool IsInWindow(MouseState mouseState)
        {
            return _windowRegion.Contains(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Construct a new simple mouse controller with added options
        /// </summary>
        public MouseTools(GraphicsDevice graphicsDevice)
        {
            int gameHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int gameWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _windowRegion = new Rectangle(0, 0, gameHeight, gameWidth);
            _font = Texture2DManager.GetSpriteFont("itemfont");
            _currentMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _mousePositionText = $"<{_currentMousePosition.X}, {_currentMousePosition.Y}>";
            _pixels = new Texture2D(graphicsDevice, 1, 1);
            _pixels.SetData(new[] { Color.White });
        }

        /// <summary>
        /// Draw mouse coordinates on screen
        /// </summary>
        /// <param name="spriteBatch">The current sprite batch</param>
        public void DrawCoordinates(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(_font, _mousePositionText, _currentMousePosition, Color.White, 0, _currentMousePosition, 1.0f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draw a rectangle where you click (not implemented as of right now to update)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawClickedRectangleCoordinates(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pixels, new Rectangle(_positionRectangle.X, _positionRectangle.Y, _positionRectangle.Width, 1), _positionRectangleColor);
            spriteBatch.Draw(_pixels, new Rectangle(_positionRectangle.X, _positionRectangle.Y, 1, _positionRectangle.Height), _positionRectangleColor);
            // Draw the bottom side of the collider
            spriteBatch.Draw(_pixels, new Rectangle(_positionRectangle.X, _positionRectangle.Y + _positionRectangle.Height, _positionRectangle.Width, 1), _positionRectangleColor);
            // Draw the right side of the collider
            spriteBatch.Draw(_pixels, new Rectangle(_positionRectangle.X + _positionRectangle.Width, _positionRectangle.Y, 1, _positionRectangle.Height), _positionRectangleColor);
            spriteBatch.DrawString(_font, _mousePositionText, _pixelPosition, Color.White, 0, _pixelPosition, 1.0f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Update the coordinates as the mouse moves
        /// </summary>
        public void UpdateCoordinates()
        {
            MouseState currentMouseState = Mouse.GetState();
            if (!IsInWindow(currentMouseState) || _previousMouseState == currentMouseState) { return; }
            /* update the mouse coordinates based off current state */
            _currentMousePosition.X = currentMouseState.X / ScaleFactor;
            _currentMousePosition.Y = currentMouseState.Y / ScaleFactor;
            // off because I'm using mouse's position. We need a starting position that updates based on mouse being left or right.
            _mousePositionText = $"<{_currentMousePosition.X}, {_currentMousePosition.Y}>";
            _previousMouseState = currentMouseState;
        }
    }
}

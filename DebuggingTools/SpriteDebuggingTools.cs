using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SprintZero1.DebuggingTools
{
    /// <summary>
    /// Simple debugging tool for sprites and objects
    /// </summary>
    internal class SpriteDebuggingTools
    {
        readonly Texture2D _pixels;
        public SpriteDebuggingTools(Game game)
        {
            _pixels = new Texture2D(game.GraphicsDevice, 1, 1);
            _pixels.SetData(new[] { Color.White });
        }
        /// <summary>
        /// Draws a rectangle around the list rectangles given
        /// </summary>
        /// <param name="rectangles">the rectangles to be drawn</param>
        /// <param name="color">the color of the rectangles</param>
        /// <param name="spriteBatch">the spirte batch to draw the rectangles with</param>
        public void DrawRectangles(List<Rectangle> rectangles, Color color, SpriteBatch spriteBatch)
        {
            foreach (Rectangle rectangle in rectangles)
            {

                spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
                spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
                // Draw the bottom side of the collider
                spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), color);
                // Draw the right side of the collider
                spriteBatch.Draw(_pixels, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
            };
        }

        /// <summary>
        /// Draws a rectangle around the desired rectangle
        /// </summary>
        /// <param name="rectangle">the rectangle to be drawn</param>
        /// <param name="color">the color of the rectangles</param>
        /// <param name="spriteBatch">the spirte batch to draw the rectangles with</param>
        public void DrawRectangle(Rectangle rectangle, Color color, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
            spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
            // Draw the bottom side of the collider
            spriteBatch.Draw(_pixels, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), color);
            // Draw the right side of the collider
            spriteBatch.Draw(_pixels, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
        }
    }
}


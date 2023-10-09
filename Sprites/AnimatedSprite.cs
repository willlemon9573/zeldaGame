using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SprintZero1.Sprites
{
    [Serializable]
    public class AnimatedSprite : ISprite
    {
        private readonly List<Rectangle> _sourceRectangles;
        private readonly Texture2D _spriteSheet;
        private readonly int _maxFrames;
        private int _currentFrame;
        private float _timeElapsed, _timeToUpdate;
        private bool _paused;

        /// <summary>
        /// Update how many frames should display per second
        /// </summary>
        private int FramesPerSecond
        {
            set
            {
                _timeToUpdate = (1f / value);
            }
        }

        /// <summary>
        /// Handles the animation for the _sprite
        /// </summary>
        /// <param name="deltaTime">Total time elapsed since the previous frame</param>
        private void Animate(float deltaTime)
        {
            /* Calculate the total time that has passed since the previous update */
            _timeElapsed += deltaTime;
            /* Move to the next frame when enough time has passed */
            if (_timeElapsed > _timeToUpdate)
            {
                _timeElapsed -= _timeToUpdate;
                _currentFrame = (_currentFrame + 1) % _maxFrames;
            }
        }
        /// <summary>
        /// Constructor for creating an animated _sprite
        /// </summary>
        /// <param name="sourceRectangles">The source rectangle list of frames for the animated _sprite</param>
        /// <param name="texture">the _sprite sheet to use when drawing the _sprite</param>
        /// <param name="maxFrames">The maximum amount of frames for the _sprite</param>
        public AnimatedSprite(List<Rectangle> sourceRectangles, Texture2D spriteSheet, int maxFrames, bool paused)
        {
            /* May want to consider a fourth parameter for time to update */
            _sourceRectangles = sourceRectangles;
            _spriteSheet = spriteSheet;
            _maxFrames = maxFrames;
            _currentFrame = 0;
            FramesPerSecond = 5;
            _paused = paused;
        }
        /// <summary>
        /// Start the animation
        /// </summary>
        public void Start()
        {
            _paused = true;
        }

        /// <summary>
        /// Stop the animation
        /// </summary>
        public void Stop()
        {
            _paused = false;
        }

        /// <summary>
        /// Resets animations
        /// </summary>
        public void Reset()
        {
            _currentFrame = 0;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f)
        {
            /* Build the source rectangle and destination rectangle to draw onto screen */
            Rectangle sourceRectangle = _sourceRectangles[_currentFrame];
            int height = sourceRectangle.Height;
            int width = sourceRectangle.Width;
            Vector2 origin = new Vector2(width / 2, height / 2); /* origin of the drawing in the middle for rotation */
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(width), (int)(height));
            spriteBatch.Draw(_spriteSheet, destinationRectangle, sourceRectangle, Color.White, rotation, origin, spriteEffects, 0f);
        }

        public void Update(GameTime gameTime)
        {
            if (!_paused)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Animate(deltaTime);
            }
        }
    }
}

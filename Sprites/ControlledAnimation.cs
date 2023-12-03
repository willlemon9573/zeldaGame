using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SprintZero1.Sprites
{
    /// <summary>
    /// Represents an animation that can be controlled in terms of its duration and stopping condition.
    /// This class provides functionality to animate a sprite for a specified number of frames.
    /// </summary>
    /// <author>Zihe Wang</author>
    [Serializable]
    public class ControlledAnimation : ISprite
    {
        private readonly AnimatedSprite _animatedSprite; // The underlying animated sprite
        private bool _shouldStop; // Flag to indicate whether the animation should stop
        private float _timeToUpdate; // Time interval for updating the animation frame
        private readonly int _maxFrames; // Maximum number of frames for the animation
        private float _totalElapsedTime = 0f; // Total elapsed time for the animation
        private int FramesPerSecond
        {
            set { _timeToUpdate = (1f / value); }
        }

        public int Width { get => _animatedSprite.Width; }

        public int Height { get => _animatedSprite.Height; }

        public ControlledAnimation(AnimatedSprite animatedSprite, int maxFrames)
        {
            _animatedSprite = animatedSprite;

            _shouldStop = false;
            _maxFrames = maxFrames;
            FramesPerSecond = 8; // Default frame rate
        }

        public void Update(GameTime gameTime)
        {
            if (!_shouldStop)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _totalElapsedTime += deltaTime;
                _animatedSprite.Update(gameTime);

                if (_totalElapsedTime >= _maxFrames * _timeToUpdate)
                {
                    _shouldStop = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0f)
        {
            if (!_shouldStop)
            {
                _animatedSprite.Draw(spriteBatch, position, Color.White);
            }
        }
    }
}

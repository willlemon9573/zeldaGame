using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SprintZero1.Sprites
{
    [Serializable]
    public class ControlledAnimation : ISprite
    {
        private AnimatedSprite _animatedSprite;
        private bool _shouldStop;
        private int _frameCount;
        private float _timeToUpdate;
        private int _maxFrames;
        private float _totalElapsedTime = 0f;
        private int FramesPerSecond
        {
            set
            {
                _timeToUpdate = (1f / value);
            }
        }

        public ControlledAnimation(AnimatedSprite animatedSprite, int maxFrames)
        {
            _animatedSprite = animatedSprite;
            _frameCount = 0;
            _shouldStop = false;
            _maxFrames = maxFrames;
            FramesPerSecond = 8;
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

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f, float layerDepth = 0f)
        {
            if (!_shouldStop)
            {
                _animatedSprite.Draw(spriteBatch, position);
            }
        }
    }

}

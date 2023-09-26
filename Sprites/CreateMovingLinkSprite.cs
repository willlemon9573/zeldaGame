
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1.Sprites
{
    public class CreateMovingLinkSprite : ISprite
    {
        private readonly List<Rectangle> sourceRectangles;  // Storing all the rectangles for animation
        private readonly Vector2 location;
        private readonly Texture2D spriteSheet;
        private readonly int _direction;
        private bool _isAttacking;
        private const int endAttackFrame = 5;
        private double elapsedTime;
        private const double timePerFrame = 500;
        private Rectangle destinationRectangle1;

        // To track which frame/rectangle to use
        private int currentFrameIndex;

        public CreateMovingLinkSprite(List<Rectangle> spritePositions, Texture2D LinkSpriteSheet, Vector2 position, int frameIndex, int direction, bool isAttacking)
        {
            this.sourceRectangles = spritePositions;
            this.spriteSheet = LinkSpriteSheet;
            this.location = position;
            this.currentFrameIndex = frameIndex;  
            this._direction = direction;
            this._isAttacking = isAttacking;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle1 = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            // If _direction is 2, flip the sprite; otherwise, use the original orientation.
            SpriteEffects effect = (_direction == 2) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, destinationRectangle1, sourceRectangles[currentFrameIndex], Color.White, 0f, Vector2.Zero, effect, 0f);
            spriteBatch.End();
        }


        public void Update(GameTime gameTime)
        {
            if (_isAttacking)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsedTime > timePerFrame)
                {
                    currentFrameIndex++;
                    
                    elapsedTime = 0;

                    if (currentFrameIndex > endAttackFrame)
                    {
                        currentFrameIndex = 1;  // Reset to the first frame after the attack animation is done
                        _isAttacking = false;
                    }
                }
            }
        }
    }

}



using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Controllers;

namespace SprintZero1.Sprites
{
    public class CreateMovingLinkSprite : ISprite
    {
        private readonly List<Rectangle> sourceRectangles;  // Storing all the rectangles for animation
        private readonly Vector2 location;
        private readonly Texture2D spriteSheet;
        private readonly int _direction;
        private bool _isAttacking;
        private const int endAttackFrame = 2;
        private double elapsedTime;
        private const double timePerFrame = 500;
        private Rectangle destinationRectangle1;
        private Color tint;
        private float hurtTimer = 0;
        private bool isHurt = false; 

        // To track which frame/rectangle to use
        private int currentFrameIndex;

        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CreateMovingLinkSprite(List<Rectangle> spritePositions, Texture2D LinkSpriteSheet, Vector2 position, int frameIndex, int direction, bool isAttacking)
        {
            this.sourceRectangles = spritePositions;
            this.spriteSheet = LinkSpriteSheet;
            this.location = position;
            this.currentFrameIndex = frameIndex;  
            this._direction = direction;
            this._isAttacking = isAttacking;
            this.tint = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle1 = new Rectangle((int)location.X, (int)location.Y, 49, 49);
            // If _direction is 2, flip the sprite; otherwise, use the original orientation.
            SpriteEffects effect = (_direction == 2) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(spriteSheet, destinationRectangle1, sourceRectangles[currentFrameIndex], tint, 0f, Vector2.Zero, effect, 0f);
        }

        public void Hurt()
        {
            isHurt = true;
            tint = Color.Red;
        }
        public void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Hurt();
            }
            if (isHurt)
            {
                Debug.WriteLine("Hurting Link " + this);
                hurtTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                Debug.WriteLine(hurtTimer);
                if(hurtTimer > 0.7f)
                {
                    tint = Color.White;
                    Debug.WriteLine("Done Hurting Link " + this);
                    hurtTimer = 0;
                    isHurt = false;
                }
            }
            if (_isAttacking)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                Debug.WriteLine("Link Attacking");

                if (elapsedTime > timePerFrame)
                {
                    Debug.WriteLine("Link Attacked " + this);
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


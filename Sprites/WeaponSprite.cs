
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SprintZero1.Sprites
{
    public class WeaponSprite : ISprite
    {
        private List<Rectangle> sourceRectanglesList;
        private Vector2 location;
        private readonly Texture2D spriteSheet;
        private readonly int direction;
        private int currentFrameIndex;
        private int maxFrame;
        private double elapsedTime;
        private const double timePerFrame = 500;
        SpriteEffects effect;
        float rotation
        
        public Vector2 Location { get { return location; } set { location = value; } }
        //vector2 location, Rectangle sourceRectangle, Texture2D spriteSheet, int maxFrame
        public WeaponSprite(Vector2 location, Rectangle sourceRectangle, Texture2D spriteSheet, int maxFrame, int direction)
        {
            currentFrameIndex = 0;
            sourceRectanglesList = new List<Rectangle>();
            Rectangle tempSourceRectangle = sourceRectangle;
            for (int i = 0; i < maxFrame; i++)
            {
                sourceRectanglesList.Add(tempSourceRectangle);
                tempSourceRectangle = new Rectangle(tempSourceRectangle.X + tempSourceRectangle.Width, tempSourceRectangle.Y, tempSourceRectangle.Width, tempSourceRectangle.Height);
            }
            this.location = location;
            this.spriteSheet = spriteSheet;
            this.maxFrame = maxFrame;
            this.direction = direction;
            switch (direction)
            {
                case 0: // Up
                    effect = SpriteEffects.None;
                    rotation = MathHelper.ToRadians(90);
                    break;

                case 1: // Down
                    effect = SpriteEffects.None;
                    rotation = MathHelper.ToRadians(-90);
                    break;

                case 3: // Left
                    effect = SpriteEffects.None;
                    rotation = 0f;
                    break;

                case 2: // Right
                    effect = SpriteEffects.FlipHorizontally;
                    rotation = 0f;
                    break;

                default:
                    effect = SpriteEffects.None;
                    rotation = 0f;
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, location, sourceRectanglesList[currentFrameIndex], Color.White, rotation, new Vector2(0, 0), 1.0f, effect, 0f);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timePerFrame)
            {
                currentFrameIndex++;
                
                elapsedTime = 0;

                if (currentFrameIndex >= maxFrame)
                {
                    currentFrameIndex = 0;

                    if (maxFrame == 3)
                    {
                        if(direction == -1)
                        {
                            direction = 2;
                        }
                        else
                        {
                            direction = -1;
                        }
                    }
                    
                }
            }
        }
    }
}



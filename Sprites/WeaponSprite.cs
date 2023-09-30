using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SprintZero1.Sprites
{
    public class WeaponSprite : ISprite
    {
        /* Temporary Weapon Sprite class */
        private List<Rectangle> sourceRectanglesList;
        private Vector2 location;
        private readonly Texture2D spriteSheet;
        private int currentFrameIndex;
        private int maxFrame;
        private float timeElapsed, timeToUpdate;
        private SpriteEffects effect;
        private float rotation;

        public Vector2 Location { 
            get { return location; } 
            set { location += value; } 
        }
        private int FramesPerSecond
        {
            set { timeToUpdate = (1f / value);  }
        }
        //vector2 location, Rectangle sourceRectangle, Texture2D spriteSheet, int maxFrame
        public WeaponSprite(Vector2 location, List<Rectangle> sourceRectangle, Texture2D spriteSheet, int maxFrame, int direction)
        {
            currentFrameIndex = 0;
            sourceRectanglesList = new List<Rectangle>();
            /* Changed the code from iterating through the list itself to taking in a list of specific frame data */
            /* Rectangle tempSourceRectangle = sourceRectangle;
            for (int i = 0; i < maxFrame; i++)
            {
                sourceRectanglesList.Add(tempSourceRectangle);
                tempSourceRectangle = new Rectangle(tempSourceRectangle.X + tempSourceRectangle.Width, tempSourceRectangle.Y, tempSourceRectangle.Width, tempSourceRectangle.Height);
            }*/
            this.sourceRectanglesList = sourceRectangle;
            FramesPerSecond = 10;
            this.location = location;
            this.spriteSheet = spriteSheet;
            this.maxFrame = maxFrame;
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
                case 2: // Left
                    effect = SpriteEffects.None;
                    rotation = 0f;
                    break;
                case 3: // Right
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
            /* Frames can differ - use the specific frame width and height to build the destination rectangle */
            int texture_width = sourceRectanglesList[currentFrameIndex].Width;
            int texture_height = sourceRectanglesList[currentFrameIndex].Height;
            /* Destination rectangle multiplied by a scale of 3 to try to create a "bigger" sprite" */
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, texture_width * 3, texture_height * 3);
            spriteBatch.Draw(spriteSheet, destinationRectangle, sourceRectanglesList[currentFrameIndex], Color.White, rotation, new Vector2(texture_width/2, texture_height/2), effect, 0f);
            /* spriteBatch.Draw(spriteSheet, location, sourceRectanglesList[currentFrameIndex], Color.White, rotation, new Vector2(0, 0), 1.0f, effect, 0f); */
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeElapsed += deltaTime;
            /* use delta time and timeElapsed to have the spirte update accordingly */
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                currentFrameIndex++;
                if (currentFrameIndex >= maxFrame)
                {
                    currentFrameIndex = 0;
                }
            }
        }
    }
}

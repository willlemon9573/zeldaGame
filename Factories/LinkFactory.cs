using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class LinkFactory : ILinkFactory
    {
        private Texture2D LinkSpriteSheet;
        private readonly Dictionary<int, List<Rectangle>> spritePositions;
        const int WIDTH = 15, HEIGHT = 15;

        private void CreateLinkDictionary()
        {
            // Move Down
            spritePositions[1] = new List<Rectangle>
            {
                new Rectangle(1, 11, WIDTH, HEIGHT),
                new Rectangle(18, 11, WIDTH, HEIGHT)
            };

            // Move Right
            spritePositions[3] = new List<Rectangle>
            {
                new Rectangle(35, 11, WIDTH, HEIGHT),
                new Rectangle(52, 11, WIDTH, HEIGHT)
            };

            // Move Left
            spritePositions[2] = spritePositions[3];

            // Move Up
            spritePositions[0] = new List<Rectangle>
            {
                new Rectangle(69, 11, WIDTH, HEIGHT),
                new Rectangle(86, 11, WIDTH, HEIGHT)
            };

            // Attack Down
            spritePositions[1].AddRange(new List<Rectangle>
            {
                new Rectangle(1, 47, WIDTH, HEIGHT),
                new Rectangle(18, 47, WIDTH, HEIGHT+10),
                new Rectangle(35, 47, WIDTH, HEIGHT+6),
                new Rectangle(52, 47, WIDTH, HEIGHT+2)
            });

            // Attack Right
            spritePositions[3].AddRange(new List<Rectangle>
            {
                new Rectangle(1, 77, WIDTH, HEIGHT),
                new Rectangle(18, 77, WIDTH+11, HEIGHT),
                new Rectangle(46, 77, WIDTH+7, HEIGHT),
                new Rectangle(70, 77, WIDTH+3, HEIGHT)
            });

            // Attack Left
            spritePositions[2].AddRange(spritePositions[3]);

            // Attack Up
            spritePositions[0].AddRange(new List<Rectangle>
            {
                new Rectangle(1, 109, WIDTH, HEIGHT),
                new Rectangle(18, 97, WIDTH, HEIGHT+12),
                new Rectangle(35, 98, WIDTH, HEIGHT+11),
                new Rectangle(52, 106, WIDTH, HEIGHT+3)
            });
        }



        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        public LinkFactory()
        { 
            spritePositions = new Dictionary<int, List<Rectangle>>();
            CreateLinkDictionary();
        }

        public void LoadTextures(ContentManager manager)
        {
            LinkSpriteSheet = manager.Load<Texture2D>("8366");
        }

        public ISprite createNewLink(int direction, Vector2 position, int frameIndex)
        {
            Debug.Assert(spritePositions.ContainsKey(direction), "Direction does not exist");


            // Get the appropriate rectangle based on direction and frame
            List<Rectangle> spriteRectangle = spritePositions[direction];

            return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex);
        }

    }
}

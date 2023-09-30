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
        const int WIDTH = 16, HEIGHT = 16;

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
                new Rectangle(107, 11, WIDTH, HEIGHT)
            });

            // Attack Right
            spritePositions[3].AddRange(new List<Rectangle>
            {
                new Rectangle(124, 11, WIDTH, HEIGHT)
            });

            // Attack Left
            spritePositions[2].AddRange(spritePositions[3]);

            // Attack Up
            spritePositions[0].AddRange(new List<Rectangle>
            {
                new Rectangle(141, 11, WIDTH, HEIGHT)
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

        public ISprite createNewLink(int direction, Vector2 position, int frameIndex,bool isAttacking)
        {
            Debug.Assert(spritePositions.ContainsKey(direction), "Direction does not exist");


            // Get the appropriate rectangle based on direction and frame
            List<Rectangle> spriteRectangle = spritePositions[direction];

            return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex, direction, isAttacking);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class LinkFactory
    {
        private Texture2D LinkSpriteSheet;
        private readonly Dictionary<Direction, ISprite> spritePositions;
        const int WIDTH = 16, HEIGHT = 16;

        private void CreateLinkDictionary()
        {
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
            const int MAX_FRAMES = 2;
            const bool PAUSED = false;
            // Move Down
            List<Rectangle> spriteRectangle = new List<Rectangle> { new Rectangle(1, 11, WIDTH, HEIGHT), new Rectangle(18, 11, WIDTH, HEIGHT) };
            spritePositions.Add(Direction.South, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Right
            spriteRectangle = new List<Rectangle> { new Rectangle(35, 11, WIDTH, HEIGHT), new Rectangle(52, 11, WIDTH, HEIGHT) };
            spritePositions.Add(Direction.East, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Left - Uses the same price as east, but flipped horizontally
            spritePositions.Add(Direction.West, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Up
            spriteRectangle = new List<Rectangle> { new Rectangle(69, 11, WIDTH, HEIGHT), new Rectangle(86, 11, WIDTH, HEIGHT) };
            spritePositions.Add(Direction.North, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        public LinkFactory()
        {
            spritePositions = new Dictionary<Direction, ISprite>();
        }

        public void LoadTextures(ContentManager manager)
        {
            CreateLinkDictionary();
            LinkSpriteSheet = manager.Load<Texture2D>("8366");
        }

        public ISprite createNewLink(Direction direction, Vector2 position, int frameIndex, bool isAttacking)
        {
            Debug.Assert(spritePositions.ContainsKey(direction), "Direction does not exist");

            /*return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex, direction, isAttacking);*/
            return spritePositions[direction];
        }

    }
}

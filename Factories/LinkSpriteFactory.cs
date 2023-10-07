using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class LinkSpriteFactory
    {
        private Texture2D LinkSpriteSheet;
        private readonly Dictionary<Direction, ISprite> movementDictionary;
        private static readonly LinkSpriteFactory instance = new();
        public static LinkSpriteFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Create the Link Dictionary that will contain links Movement Sprites
        /// </summary>
        private void CreateMovementSpriteDictionary()
        {
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
            const int MAX_FRAMES = 2, WIDTH = 16, HEIGHT = 16;
            const bool PAUSED = false;
            // Move Down
            List<Rectangle> spriteRectangle = new() { new Rectangle(1, 11, WIDTH, HEIGHT), new Rectangle(18, 11, WIDTH, HEIGHT) };
            movementDictionary.Add(Direction.South, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Right
            spriteRectangle = new() { new Rectangle(35, 11, WIDTH, HEIGHT), new Rectangle(52, 11, WIDTH, HEIGHT) };
            movementDictionary.Add(Direction.East, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Left - Uses the same price as east, but flipped horizontally
            movementDictionary.Add(Direction.West, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
            // Move Up
            spriteRectangle = new() { new Rectangle(69, 11, WIDTH, HEIGHT), new Rectangle(86, 11, WIDTH, HEIGHT) };
            movementDictionary.Add(Direction.North, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES, PAUSED));
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private LinkSpriteFactory()
        {
            movementDictionary = new Dictionary<Direction, ISprite>();
        }

        public void LoadTextures()
        {
            CreateMovementSpriteDictionary();
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
        }

        public ISprite GetLinkSprite(Direction direction)
        {
            Debug.Assert(movementDictionary.ContainsKey(direction), "Direction does not exist");

            /*return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex, direction, isAttacking);*/
            return movementDictionary[direction];
        }

    }
}

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
        private readonly Dictionary<Direction, ISprite> movementSpriteDictionary;
        private readonly Dictionary<Direction, ISprite> attackSpriteDictionary;
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
            // Move Down
            List<Rectangle> spriteRectangle = new() { new Rectangle(1, 11, WIDTH, HEIGHT), new Rectangle(18, 11, WIDTH, HEIGHT) };
            movementSpriteDictionary.Add(Direction.South, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES));
            // Move Right
            spriteRectangle = new() { new Rectangle(35, 11, WIDTH, HEIGHT), new Rectangle(52, 11, WIDTH, HEIGHT) };
            movementSpriteDictionary.Add(Direction.East, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES));
            // Move Left - Uses the same price as east, but flipped horizontally
            movementSpriteDictionary.Add(Direction.West, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES));
            // Move Up
            spriteRectangle = new() { new Rectangle(69, 11, WIDTH, HEIGHT), new Rectangle(86, 11, WIDTH, HEIGHT) };
            movementSpriteDictionary.Add(Direction.North, new AnimatedSprite(spriteRectangle, LinkSpriteSheet, MAX_FRAMES));
        }

        private void CreateAttackingFrameDictionary()
        {
            const int X_OFFSET = 17, Y_ORIGIN = 11, HEIGHT = 16, WIDTH = 16; /* 16 + 1*/
            int x_origin = 107;
            // Attack south
            Rectangle attackingFrames = new Rectangle(x_origin, Y_ORIGIN, WIDTH, HEIGHT);
            attackSpriteDictionary.Add(Direction.South, new NonAnimatedSprite(attackingFrames, LinkSpriteSheet));
            // Atack right
            x_origin += X_OFFSET;
            attackingFrames = new Rectangle(x_origin, Y_ORIGIN, WIDTH, HEIGHT);
            attackSpriteDictionary.Add(Direction.East, new NonAnimatedSprite(attackingFrames, LinkSpriteSheet));
            // Attack left (flipped rip)
            attackSpriteDictionary.Add(Direction.West, new NonAnimatedSprite(attackingFrames, LinkSpriteSheet));
            // attack North
            x_origin += X_OFFSET;
            attackingFrames = new Rectangle(x_origin, Y_ORIGIN, WIDTH, HEIGHT);
            attackSpriteDictionary.Add(Direction.North, new NonAnimatedSprite(attackingFrames, LinkSpriteSheet));

        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private LinkSpriteFactory()
        {
            movementSpriteDictionary = new Dictionary<Direction, ISprite>();
            attackSpriteDictionary = new Dictionary<Direction, ISprite>();
        }

        public void LoadTextures()
        {
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
            CreateMovementSpriteDictionary();
            CreateAttackingFrameDictionary();
        }

        public ISprite GetLinkSprite(Direction direction)
        {
            Debug.Assert(movementSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");

            /*return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex, direction, isAttacking);*/
            return movementSpriteDictionary[direction];
        }

        public ISprite GetAttackingSprite(Direction direction)
        {
            Debug.Assert(attackSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");
            return attackSpriteDictionary[direction];
        }

    }
}

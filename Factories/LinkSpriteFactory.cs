using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class LinkSpriteFactory
    {
        private Texture2D LinkSpriteSheet;
        private readonly Dictionary<Direction, List<Rectangle>> movementSpriteDictionary;
        private readonly Dictionary<Direction, Rectangle> attackSpriteDictionary;
        private static readonly LinkSpriteFactory instance = new();
        public static LinkSpriteFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private LinkSpriteFactory()
        {
            movementSpriteDictionary = FactoryXMLParser.ParseAnimatedSpriteWithDirectionXML("LinkMovingSprites.xml");
            attackSpriteDictionary = FactoryXMLParser.ParseNonAnimatedSpriteWithDirectionXML("LinkAttackingSprites.xml");
        }

        public void LoadTextures()
        {
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
        }

        public ISprite GetLinkSprite(Direction direction)
        {
            Debug.Assert(movementSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");

            /*return new CreateMovingLinkSprite(spriteRectangle, LinkSpriteSheet, position, frameIndex, direction, isAttacking);*/
            return new AnimatedSprite(movementSpriteDictionary[direction], LinkSpriteSheet, 2);
        }

        public ISprite GetAttackingSprite(Direction direction)
        {
            Debug.Assert(attackSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");
            return new NonAnimatedSprite(attackSpriteDictionary[direction], LinkSpriteSheet);
        }

    }
}

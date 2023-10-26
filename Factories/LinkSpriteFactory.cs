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
        /// <summary>
        /// Get the instance of the factory
        /// </summary>
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
        /// <summary>
        /// Load the required sprite sheet that contains Link's sprite data
        /// </summary>
        public void LoadTextures()
        {
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
        }
        /// <summary>
        /// Create and return a new sprite for link moving
        /// </summary>
        /// <param name="direction">The direction in which link will be facing</param>
        /// <returns></returns>
        public ISprite GetLinkSprite(Direction direction)
        {
            Debug.Assert(movementSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");
            return new AnimatedSprite(movementSpriteDictionary[direction], LinkSpriteSheet, 2);
        }
        /// <summary>
        /// Create and return a new attacking sprite for link
        /// </summary>
        /// <param name="direction">The direction in which link will be facing</param>
        /// <returns></returns>
        public ISprite GetAttackingSprite(Direction direction)
        {
            Debug.Assert(attackSpriteDictionary.ContainsKey(direction), "Direction not found in dictionary");
            return new NonAnimatedSprite(attackSpriteDictionary[direction], LinkSpriteSheet);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class ItemSpriteFactory
    {
        private const string ANIMATED_ITEMS_DOCUMENT_PATH = @"XMLFILES/FactoryXMLFiles/AnimatedItemSprites.xml";
        private const string NONANIMATED_ITEMS_DOCUMENT_PATH = @"XMLFiles/FactoryXMLFiles/NonAnimatedItemSprites.xml";

        private Texture2D itemSpriteSheet;
        private static readonly ItemSpriteFactory instance = new ItemSpriteFactory();
        private readonly Dictionary<string, List<Rectangle>> AnimatedItemSpriteMap;
        private readonly Dictionary<string, Rectangle> NonAnimatedItemSpriteMap;

        /// <summary>
        /// 
        /// </summary>
        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Private constructor so factory cannot be instantiated
        /// </summary>
        private ItemSpriteFactory()
        {
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            AnimatedItemSpriteMap = spriteParser.ParseAnimatedSpriteXML(ANIMATED_ITEMS_DOCUMENT_PATH);
            NonAnimatedItemSpriteMap = spriteParser.ParseNonAnimatedSpriteXML(NONANIMATED_ITEMS_DOCUMENT_PATH);
        }

        /// <summary>
        /// Load the sprite sheet containing all the item sprites
        /// </summary>
        public void LoadTextures()
        {
            itemSpriteSheet = Texture2DManager.GetItemSpriteSheet();
        }

        /// <summary>
        /// Create and return a new animated item sprite
        /// </summary>
        /// <param name="itemName">The name of the specific item</param>
        /// <returns></returns>
        public ISprite CreateAnimatedItemSprite(string itemName, int maxFrames)
        {
            return new AnimatedSprite(AnimatedItemSpriteMap[itemName], itemSpriteSheet, maxFrames);
        }

        /// <summary>
        /// Create and return a new non-animated item sprite
        /// </summary>
        /// <param name="itemName">the name of the specific item</param>
        /// <returns></returns>
        public ISprite CreateNonAnimatedItemSprite(string itemName)
        {
            return new NonAnimatedSprite(NonAnimatedItemSpriteMap[itemName], itemSpriteSheet);
        }

        /// <summary>
        /// Gets the dimensions of the specific animated item for use with collision
        /// </summary>
        /// <param name="itemName">The item to obtain the dimensions of</param>
        /// <returns>A rectangle containing the dimensions of the item being requested</returns>
        public Rectangle GetAnimatedSpriteDimensions(string itemName)
        {
            int firstFrame = 0;
            // get the first rectangle of the sprite as this should contain the proper dimensions
            Rectangle spriteDimensions = AnimatedItemSpriteMap[itemName][firstFrame];
            return spriteDimensions;
        }
    }
}

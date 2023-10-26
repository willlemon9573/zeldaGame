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
            AnimatedItemSpriteMap = FactoryXMLParser.ParseAnimatedSpriteXML("AnimatedItemSprites.XML");
            NonAnimatedItemSpriteMap = FactoryXMLParser.ParseNonAnimatedSpriteXML("NonAnimatedItemSprites.XML");
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
        public ISprite CreateAnimatedItemSprite(string itemName)
        {
            return new AnimatedSprite(AnimatedItemSpriteMap[itemName], itemSpriteSheet, 2);
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
    }
}

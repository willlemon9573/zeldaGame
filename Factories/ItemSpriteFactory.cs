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
#pragma warning disable IDE0090 // Use 'new(...)'
        private static readonly ItemSpriteFactory instance = new ItemSpriteFactory();
#pragma warning restore IDE0090 // Use 'new(...)'
        private readonly Dictionary<string, List<Rectangle>> AnimatedItemSpriteMap;
        private readonly Dictionary<string, Rectangle> NonAnimatedItemSpriteMap;
        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        private ItemSpriteFactory()
        {
            AnimatedItemSpriteMap = FactoryXMLParser.ParseAnimatedSpriteXML(@"..\..\..\XMLFiles\FactoryXMLFiles\AnimatedItemSprites.XML");
            NonAnimatedItemSpriteMap = FactoryXMLParser.ParseNonAnimatedSpriteXML(@"..\..\..\XMLFiles\FactoryXMLFiles\NonAnimatedItemSprites.XML");
        }

        public void LoadTextures()
        {
            itemSpriteSheet = Texture2DManager.GetItemSpriteSheet();
        }

        public ISprite CreateAnimatedItemSprite(string itemName)
        {
            return new AnimatedSprite(AnimatedItemSpriteMap[itemName], itemSpriteSheet, 2);
        }
        public ISprite CreateNonAnimatedItemSprite(string itemName)
        {
            return new NonAnimatedSprite(NonAnimatedItemSpriteMap[itemName], itemSpriteSheet);
        }
    }
}

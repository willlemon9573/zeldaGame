using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Factories
{
    internal class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
#pragma warning disable IDE0090 // Use 'new(...)'
        private static readonly ItemSpriteFactory instance = new ItemSpriteFactory();
#pragma warning restore IDE0090 // Use 'new(...)'
        private readonly Dictionary<string, List<Rectangle>> itemSpriteDictionary;
        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        public List<string> ItemNamesList
        {
            get { return itemSpriteDictionary.Keys.ToList<string>(); }
        }
        private void CreateItemSpriteDictionary()
        {
            itemSpriteDictionary["key"] = new List<Rectangle>
            {
                new Rectangle(228, 703, 11, 17),
                new Rectangle(228, 703, 11, 17)

            };
            itemSpriteDictionary["map"] = new List<Rectangle>
            {
                new Rectangle(245, 703, 11, 17),
                new Rectangle(245, 703, 11, 17)

            };
            itemSpriteDictionary["compass"] = new List<Rectangle>
            {
                new Rectangle(260, 703, 14, 17),
                new Rectangle(260, 703, 14, 17)

            };
            itemSpriteDictionary["fire"] = new List<Rectangle>
            {
                new Rectangle(312, 703, 15, 17),
                new Rectangle(329, 703, 14, 17)

            };
        }

        private ItemSpriteFactory()
        {
            itemSpriteDictionary = new Dictionary<string, List<Rectangle>>();
            CreateItemSpriteDictionary();
        }

        public void LoadTextures()
        {
            itemSpriteSheet = Texture2DManager.GetItemSpriteSheet();
        }

        public ISprite CreateItemSprite(string itemName)
        {
            return new AnimatedSprite(itemSpriteDictionary[itemName], itemSpriteSheet, 2);
        }

    }
}

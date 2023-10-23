using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
        private readonly Dictionary<string, Rectangle> sourceRectangles;
        private static readonly ItemSpriteFactory instance = new ItemSpriteFactory();
        private readonly List<string> itemNamesList;
        private Texture2D linkWeaponSpriteSheet;
        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        public List<string> ItemNamesList
        {
            get { return itemNamesList; }
        }
        private void CreateSourceRectanglesDictionary()
        {
            int x_pixels = 23, y_pixels = 704; // starting coordiantes of the tiles
            const int WIDTH = 16, HEIGHT = 16; // dimmension of each tile
            foreach (string itemName in ItemNamesList)
            {

                sourceRectangles.Add(itemName, new Rectangle(x_pixels, y_pixels, WIDTH, HEIGHT));
                if (itemName.Contains("Animated"))
                {
                    x_pixels += 34;
                }
                else
                {
                    x_pixels += 17;
                }

                if (x_pixels > 313)
                {
                    x_pixels = 23;
                }
            }

            Rectangle woodenSwordSource = new Rectangle(1, 154, 7, 16);
            sourceRectangles.Add("woodsword", woodenSwordSource);
        }


        private ItemSpriteFactory()
        {
            itemNamesList = new List<string>()
            {
                "rubyStatic", "heartAnimated", "heartStatic", "fairyAnimated",
                "clock", "rubyAnimated", "boomerang", "bomb",
                "bow", "key", "scroll", "compass", "triforceAnimated", "fireAnimated"
            };
            sourceRectangles = new Dictionary<string, Rectangle>();
            CreateSourceRectanglesDictionary();
        }

        public void LoadTextures()
        {
            linkWeaponSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
            itemSpriteSheet = Texture2DManager.GetItemSpriteSheet();
        }

        public ISprite CreateItemSprite(string itemName)
        {

            if (itemName.Contains("Animated"))
            {
                /*return new AnimatedItemSprite(tileSourceRectangles[itemName], itemSpriteSheet);*/
            }
            else
            {
                /*  return new NonAnimatedItemSprite(tileSourceRectangles[itemName], itemSpriteSheet);*/
            }
            return null;
        }

        public ISprite CreateNonAnimatedItemSprite(string itemName)
        {
            return new NonAnimatedSprite(sourceRectangles[itemName], linkWeaponSpriteSheet);
        }
    }
}

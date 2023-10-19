using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class ItemFactory
    {
        private Texture2D itemSpriteSheet;
        private readonly Dictionary<string, Rectangle> sourceRectangles;
        private static readonly ItemFactory instance = new ItemFactory();
        private readonly List<string> itemNamesList;
        private Texture2D linkWeaponSpriteSheet;
        public static ItemFactory Instance
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


        private ItemFactory()
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

        public void LoadTextures(ContentManager manager)
        {
            itemSpriteSheet = manager.Load<Texture2D>("itemSpriteSheet1");
            linkWeaponSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
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

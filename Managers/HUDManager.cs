using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal static class HUDManager
    {
        private static Dictionary<string, ISprite> keyValuePairs = new
                Dictionary<string, ISprite>();
        private static float heartsToDraw = 3;
        private static List<ISprite> heartSprites = new List<ISprite>();
        private static List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        public static void Initialize()
        {
            Texture2D hudSpriteSheet = Texture2DManager.GetHUDSpriteSheet();
            string path = @"XMLFiles\HUDXMLFiles\HUDSprites.xml";
            XDocument document = XDocument.Load(path);
            XElement root = document.Root; /* get root */
            XDocTools xDocTools = new XDocTools();

            foreach (XElement sprite in root.Elements("Sprite"))
            {
                /* Get the sprite name */
                string name = xDocTools.ParseAttributeAsString(sprite.Attribute("name"));
                /* Get the rectangle Element */
                XElement rectangleElement = sprite.Element("Rectangle");
                /* Parse the Rectangle Element */
                Rectangle sourceRect = xDocTools.ParseRectangleElement(rectangleElement);
                /* Create Sprite */
                ISprite hudElementSprite = new NonAnimatedSprite(sourceRect, hudSpriteSheet);
                /* Add to Dictionary */
                keyValuePairs[name] = hudElementSprite;
            }

            // Option 1 for drawing sprites
            List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
            // Option 2 to use a dictionary Sprite key, position for the value
        }

        public static void decrementHearts(float amount)
        {

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            /*Drawing Example*/
            Vector2 v = new Vector2(10, 15);
            /* Hearts will be drawn */
            foreach (var sprite in keyValuePairs.Values)
            {
                sprite.Draw(spriteBatch, v);
                v.X += 30;
            }
            // Option 2 drawing with Tuple
            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Draw(spriteBatch, sprite.Item2);
            }

        }
    }
}

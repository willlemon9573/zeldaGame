using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal static class HUDManager
    {
        private static float heartsToDraw = 3;
        private static List<ISprite> heartSprites = new List<ISprite>();
        private static List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        public static void Initialize()
        {
            string path = @"XMLFiles\HUDXMLFiles\HUDPositions.xml";
            XDocument document = XDocument.Load(path);
            XElement root = document.Root; /* get root */
            XDocTools xDocTools = new XDocTools();
            HUDSpriteFactory HUDSpriteFactoryInstance = HUDSpriteFactory.Instance;

            foreach (XElement sprite in root.Elements("Sprite"))
            {
                /* Get the sprite name */
                string name = xDocTools.ParseAttributeAsString(sprite.Attribute("name"));
                /* Get the position Element */
                XElement positionElement = sprite.Element("Vector2");
                /* Parse the Vector2 position Element */
                Vector2 position = xDocTools.ParseVector2Element(positionElement);
                /* Create Sprite */
                ISprite HUDSprite = HUDSpriteFactoryInstance.CreateHUDSprite(name);
                /* Add to List */
                spriteAndPosList.Add(new Tuple<ISprite, Vector2>(HUDSprite, position));
            }
        }

        public static void decrementHearts(float amount)
        {

        }

        public static void incrementHearts(float amount)
        {
            
        }

        public static void incrementRupees(int amount)
        {

        }

        public static void incrementBombs(int amount)
        {

        }

        public static void incrementKeys(int amount)
        {

        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            /*Drawing Example*/
            //Vector2 v = new Vector2(10, 15);
            /* Hearts will be drawn */
            //foreach (var sprite in keyValuePairs.Values)
            //{
                //sprite.Draw(spriteBatch, v);
                //v.X += 30;
            //}
            // Option 2 drawing with Tuple
            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Draw(spriteBatch, sprite.Item2);
            }

        }
    }
}

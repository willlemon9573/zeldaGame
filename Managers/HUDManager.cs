using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.InventoryFiles;
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
        private static List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        public static HUDSpriteFactory HUDSpriteFactoryInstance = HUDSpriteFactory.Instance;
        private const string Zero = "0";
        private static List<ISprite> rupeeDigits = new List<ISprite>();
        private static List<ISprite> keyDigits = new List<ISprite>();
        private static List<ISprite> bombDigits = new List<ISprite>();
        private static Dictionary<String, Vector2> positionDictionary = new Dictionary<String, Vector2>();
        private const int LeftDigitIndex = 0;
        private const int RightDigitIndex = 1;

        public static void Initialize()
        {
            string path = @"XMLFiles\HUDXMLFiles\HUDPositions.xml";
            XDocument document = XDocument.Load(path);
            XElement root = document.Root; /* get root */
            XDocTools xDocTools = new XDocTools();

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
            foreach(XElement numPosition in root.Elements("NumPosition"))
            {
                /* Get the sprite name */
                string name = xDocTools.ParseAttributeAsString(numPosition.Attribute("name"));
                /* Get the position Element */
                XElement positionElement = numPosition.Element("Vector2");
                /* Parse the Vector2 position Element */
                Vector2 position = xDocTools.ParseVector2Element(positionElement);
                positionDictionary.Add(name, position);
            }
            rupeeDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            rupeeDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            keyDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            keyDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            bombDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            bombDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
        }

        public static void decrementHearts(float amount)
        {

        }

        public static void incrementHearts(float amount)
        {
            
        }

        public static void UpdateRupeeCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            rupeeDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            rupeeDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        public static void UpdateKeyCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            keyDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            keyDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        public static void UpdateBombCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            bombDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            bombDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        public static void DrawRupeeCount(SpriteBatch spriteBatch)
        {
            rupeeDigits[LeftDigitIndex].Draw(spriteBatch, positionDictionary["rupeePosition0"]);
            rupeeDigits[RightDigitIndex].Draw(spriteBatch, positionDictionary["rupeePosition1"]);
        }

        public static void DrawKeyCount(SpriteBatch spriteBatch)
        {
            keyDigits[LeftDigitIndex].Draw(spriteBatch, positionDictionary["keyPosition0"]);
            keyDigits[RightDigitIndex].Draw(spriteBatch, positionDictionary["keyPosition1"]);
        }

        public static void DrawBombCount(SpriteBatch spriteBatch)
        {
            bombDigits[LeftDigitIndex].Draw(spriteBatch, positionDictionary["bombPosition0"]);
            bombDigits[RightDigitIndex].Draw(spriteBatch, positionDictionary["bombPosition1"]);
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
            DrawRupeeCount(spriteBatch);
            DrawKeyCount(spriteBatch);
            DrawBombCount(spriteBatch);
        }
    }
}

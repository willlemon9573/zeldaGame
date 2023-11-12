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
        private const int LeftDigitIndex = 0;
        private const int RightDigitIndex = 1;
        private const string Zero = "0";
        private static List<ISprite> rupeeDigits = new List<ISprite>();
        private static List<ISprite> keyDigits = new List<ISprite>();
        private static List<ISprite> bombDigits = new List<ISprite>();
        private static Dictionary<String, Vector2> positionDictionary = new Dictionary<String, Vector2>();


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

        public static void UpdateRupeeCount(int amount, SpriteBatch spriteBatch)
        {
            string amountString = amount.ToString();
            string firstDigit = amountString[0].ToString();
            Vector2 position0 = positionDictionary["rupeePosition0"];
            Vector2 position1 = positionDictionary["rupeePosition1"];

            if (amountString.Length == 2)
            {
                string secondDigit = amountString[1].ToString();

                rupeeDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
                rupeeDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(secondDigit);
            } 
            else
            {
                rupeeDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(Zero);
                rupeeDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
            }

            rupeeDigits[0].Draw(spriteBatch, position0);
            rupeeDigits[1].Draw(spriteBatch, position1);
        }

        public static void UpdateKeyCount(int amount, SpriteBatch spriteBatch)
        {
            string amountString = amount.ToString();
            string firstDigit = amountString[0].ToString();
            Vector2 position0 = positionDictionary["keyPosition0"];
            Vector2 position1 = positionDictionary["keyPosition1"];

            if (amountString.Length == 2)
            {
                string secondDigit = amountString[1].ToString();

                keyDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
                keyDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(secondDigit);
            }
            else
            {
                keyDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(Zero);
                keyDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
            }

            keyDigits[0].Draw(spriteBatch, position0);
            keyDigits[1].Draw(spriteBatch, position1);
        }

        public static void UpdateBombCount(int amount, SpriteBatch spriteBatch)
        {
            string amountString = amount.ToString();
            string firstDigit = amountString[0].ToString();
            Vector2 position0 = positionDictionary["bombPosition0"];
            Vector2 position1 = positionDictionary["bombPosition1"];

            if (amountString.Length == 2)
            {
                string secondDigit = amountString[1].ToString();

                bombDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
                bombDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(secondDigit);
            }
            else
            {
                bombDigits[0] = HUDSpriteFactoryInstance.CreateHUDSprite(Zero);
                bombDigits[1] = HUDSpriteFactoryInstance.CreateHUDSprite(firstDigit);
            }

            bombDigits[0].Draw(spriteBatch, position0);
            bombDigits[1].Draw(spriteBatch, position1);
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
            int rupeeCount = 99;
            int keyCount = 99;
            int bombCount = 99;
            UpdateRupeeCount(rupeeCount, spriteBatch);
            UpdateKeyCount(keyCount, spriteBatch);
            UpdateBombCount(bombCount, spriteBatch);
        }
    }
}

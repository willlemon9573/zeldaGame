using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Managers.HUDHelpers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal static class HUDManager
    {
        // list for non-health related sprites and their positions
        private static List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        private static Color DefaultColorMask;
        private static readonly Dictionary<string, Tuple<ISprite, Vector2>> _specialCaseDict = new Dictionary<string, Tuple<ISprite, Vector2>>();
        public static HUDSpriteFactory HUDSpriteFactoryInstance = HUDSpriteFactory.Instance;
        private const string Zero = "0";
        private static readonly Dictionary<string, Vector2> positionDictionary = new Dictionary<string, Vector2>();
        private const int LeftDigitIndex = 0; //array index 0
        private const int RightDigitIndex = 1; //array index 1

        /* Tracking for stackable items */
        private static Dictionary<StackableItems, Action<int>> actionMap; // contains the actions for incrementing key, bomb and rupee count
        private static readonly List<ISprite> rupeeDigits = new List<ISprite>();
        private static readonly List<ISprite> keyDigits = new List<ISprite>();
        private static readonly List<ISprite> bombDigits = new List<ISprite>();

        private static HPLinkedList playerHealth;

        /// <summary>
        /// Parses hud information to populate all the lists
        /// </summary>
        private static void ParseHUDXMLFile()
        {
            string path = @"XMLFiles/HUDXMLFiles/HUDPositions.xml";
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

                if (!name.Contains("heart"))
                {
                    if (name.Contains("map") || name.Contains("triforce"))
                    {
                        _specialCaseDict.Add(name, new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                    else
                    {
                        /* Add to List */
                        spriteAndPosList.Add(new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                }
            }


            foreach (XElement numPosition in root.Elements("NumPosition"))
            {
                /* Get the name */
                string name = xDocTools.ParseAttributeAsString(numPosition.Attribute("name"));
                /* Get the position Element */
                XElement positionElement = numPosition.Element("Vector2");
                /* Parse the Vector2 position Element */
                Vector2 position = xDocTools.ParseVector2Element(positionElement);
                positionDictionary.Add(name, position);
            }
        }

        /// <summary>
        /// Initialize lists and dictionaries needed for HUD by parsing
        /// </summary>
        public static void Initialize()
        {
            /* parse the hud information */
            ParseHUDXMLFile();
            //initialize the digits as 00 for HUD initialization
            int numberOfDigits = 2;
            for (int i = 0; i < numberOfDigits; i++)
            {
                rupeeDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
                keyDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
                bombDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            }


            /* Create action map for updating counts */
            actionMap = new Dictionary<StackableItems, Action<int>>() {
                { StackableItems.Rupee, (amount) => UpdateRupeeCount(amount)  },
                { StackableItems.Bomb, (amount) => UpdateBombCount(amount) },
                { StackableItems.DungeonKey, (amount) => UpdateKeyCount(amount) }
            };


            /* set up linked list for hearts */
            Vector2 playerHeartStartingPosition = new Vector2(180, 40);
            int playerMaxStartingHealth = 3;
            playerHealth = new HPLinkedList(playerMaxStartingHealth, playerHeartStartingPosition);
            DefaultColorMask = Color.White; // color mask for sprites (white means no mask)
        }

        /// <summary>
        /// Increases onscreen health by one
        /// </summary>
        public static void IncreasePlayerHealth()
        {
            playerHealth.IncreasePlayerHealth();
        }

        /// <summary>
        /// Decrement the player health by the given amount
        /// </summary>
        /// <param name="amount">The amount to decrement player health</param>
        public static void DecrementHealth(float amount)
        {
            playerHealth.DecrementCurrentHealth(amount);
        }

        /// <summary>
        /// Increment's the player's health
        /// </summary>
        /// <param name="amount"></param>
        /// 
        public static void IncrementHearts(float amount)
        {
            playerHealth.IncrementCurrentHealth(amount);
        }

        /// <summary>
        /// Updates Rupee count to "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of rupees</param>
        private static void UpdateRupeeCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            rupeeDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            rupeeDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        /// <summary>
        /// Updates key count to "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of keys</param>
        private static void UpdateKeyCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            keyDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            keyDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        /// <summary>
        /// Updates bomb count to "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of bombs</param>
        private static void UpdateBombCount(int amount)
        {
            int leftDigit = amount / 10;
            int rightDigit = amount % 10;
            bombDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            bombDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        /// <summary>
        /// Updates the Stackable Item's count on the HUD to the given amount
        /// </summary>
        /// <param name="itemType">The specific type of item being updated</param>
        /// <param name="amount">The amount the item is being updated to</param>
        public static void UpdateStackableItemCount(StackableItems itemType, int amount)
        {
            if (actionMap.TryGetValue(itemType, out var action))
            {
                action(amount);
            }
        }

        //makes the map visible
        public static void AddMap()
        {
            spriteAndPosList.Add(_specialCaseDict["map"]);
        }

        //makes the triforce marker visible
        public static void AddTriforceMarker()
        {
            spriteAndPosList.Add(_specialCaseDict["triforce"]);
        }

        //move the player marker depending on which room the player enters
        public static void UpdateMarker(int direction)
        {

            Vector2 markerPos = new Vector2(0, 0);
            ISprite posMarker = HUDSpriteFactory.Instance.CreateHUDSprite("player");
            foreach (var sprite in spriteAndPosList)
            {
                if (sprite.Item1 == posMarker)
                {
                    markerPos = sprite.Item2;
                }
            }
            Tuple<ISprite, Vector2> remover = new Tuple<ISprite, Vector2>(posMarker, markerPos);
            spriteAndPosList.Remove(remover);
            markerPos.X += 5f;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw everything in HUD
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch spritebatch</param>
        public static void Draw(SpriteBatch spriteBatch)
        {

            playerHealth.Draw(spriteBatch);

            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Draw(spriteBatch, sprite.Item2, DefaultColorMask);
            }

            for (int i = 0; i < 2; i++)
            {
                rupeeDigits[i].Draw(spriteBatch, positionDictionary[$"rupeePosition{i}"], DefaultColorMask);
                keyDigits[i].Draw(spriteBatch, positionDictionary[$"keyPosition{i}"], DefaultColorMask);
                bombDigits[i].Draw(spriteBatch, positionDictionary[$"bombPosition{i}"], DefaultColorMask);
            }
        }
    }
}

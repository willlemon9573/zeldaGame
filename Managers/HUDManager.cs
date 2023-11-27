using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SprintZero1.Managers
{
    internal static class HUDManager
    {
        private static List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        const int MAX_ATTAINABLE_HEALTH = 8;
        private static float[] healthArray = new float[MAX_ATTAINABLE_HEALTH];
        private static Dictionary<String, Tuple<ISprite, Vector2>> specialCaseDict = new Dictionary<String, Tuple<ISprite, Vector2>>();
        const float MapLayerDepth = 1f; // draw map on the layer depth that's considered "backgroud"
        const float AboveMapLayerDepth = 0f; // draw any other markers on the layer depth that's considered 
        const float Rotation = 0f; // because we need to add the layerdepth we also have to add rotation
        const float STARTING_HEALTH = 6f;
        const float FULL_HEART = 1f;
        const float HALF_HEART = 0.5f;
        const float EMPTY_HEART = 0f;
        const float NO_HEART = -1f;
        private static float currentHealth = 0;
        private static float maxHealth = 0;
        private static int keyCount = 0;
        private static int bombCount = 0;
        private static int rupeeCount = 0;
        private static Vector2 startingHeartPos = new Vector2(180, 40);
        public static HUDSpriteFactory HUDSpriteFactoryInstance = HUDSpriteFactory.Instance;
        private const string Zero = "0";
        private static List<ISprite> rupeeDigits = new List<ISprite>();
        private static List<ISprite> keyDigits = new List<ISprite>();
        private static List<ISprite> bombDigits = new List<ISprite>();
        private static Dictionary<String, Vector2> positionDictionary = new Dictionary<String, Vector2>();
        private const int LeftDigitIndex = 0; //array index 0
        private const int RightDigitIndex = 1; //array index 1


        /// <summary>
        /// Initialize lists and dictionaries needed for HUD by parsing
        /// </summary>
        public static void Initialize()
        {

            float[] _hearts = new float[MAX_ATTAINABLE_HEALTH];
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
                ISprite HUDSprite;
                if (name == "triforce") {
                    HUDSprite = HUDSpriteFactoryInstance.CreateAnimatedHUDSprite(name);
                }
                else { 
                    HUDSprite = HUDSpriteFactoryInstance.CreateHUDSprite(name);
                 }

                if (!name.Contains("heart"))
                {
                    if (name.Contains("map") || name.Contains("triforce"))
                    {
                        specialCaseDict.Add(name, new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                    else
                    {
                        /* Add to List */
                        spriteAndPosList.Add(new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                }

                /*Initialize hearts*/
                CreateHealth();

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
            //initialize the digits as 00 for HUD initialization
            rupeeDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            rupeeDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            keyDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            keyDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            bombDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
            bombDigits.Add(HUDSpriteFactoryInstance.CreateHUDSprite(Zero));
        }

        public static void CreateHealth()
        {
            //initializes the healthArray
            for (int i = 0; i < MAX_ATTAINABLE_HEALTH; i++) {
                //puts a full heart at each index until there are enough hearts to represent the starting health
                if (i < STARTING_HEALTH)
                {
                    healthArray[i] = FULL_HEART;
                }
                //puts a negative one for the rest of the indexes in the array
                else
                {
                    //negative ones represent nothing there
                    healthArray[i] = NO_HEART;
                }
            }
            //sets the current values of maxHealth and currentHealth
            currentHealth = 6;
            maxHealth = 6;
        }

        public static void DrawHealth(SpriteBatch a) {
            int count = 0;
            Vector2 pos = startingHeartPos;
            //finds the last index that will be drawn
            while (healthArray[count] != NO_HEART) {
                if (healthArray[count] == FULL_HEART)
                {
                    //draws full heart
                    ISprite fullHeart = HUDSpriteFactory.Instance.CreateHUDSprite("full_heart");
                    fullHeart.Draw(a, pos);
                }
                else if (healthArray[count] == HALF_HEART)
                {
                    //draws half heart
                    ISprite halfHeart = HUDSpriteFactory.Instance.CreateHUDSprite("half_heart");
                    halfHeart.Draw(a, pos);
                }
                else {
                    //draw empty heart
                    ISprite emptyHeart = HUDSpriteFactory.Instance.CreateHUDSprite("empty_heart");
                    emptyHeart.Draw(a, pos);
                }
                pos.X += 9;
                count++;
            }
        }

        //This function creates a new empty heart sprite and then heals the plaer by 1
        public static void addNewHeart()
        {
            int count = 0;
            float lastHealthValue = 0;
            int lastHealthPosition = 0;
            //finds the first index that is not a heart
            while (healthArray[count] != NO_HEART) {
               
                lastHealthPosition = count;
                lastHealthValue = healthArray[count];             
                count++;
            }
            //sets the no heart to an empty heart
            healthArray[count] = EMPTY_HEART;
            maxHealth++;
            //heals player
            IncrementHealth(1);
            
        }

        public static void DecrementHealth(float amount)
        {
            //reduces amount until decrementing will not result in negative health
            while (currentHealth - amount < 0)
            {
                amount -= 0.5f;
            }

            int count = 0;
            //finds the last index of a half/full heart
            while (healthArray[count] > EMPTY_HEART) {
                count++;
            }
            count--;
            //decrements the healthArray according on the amount and the last heart in the array
            if (amount == 0.5f)
            {
                healthArray[count] -= 0.5f;
            }
            else {
                if (healthArray[count] == HALF_HEART)
                {
                    healthArray[count] = EMPTY_HEART;
                    healthArray[count - 1] = HALF_HEART;
                }
                else
                {
                    healthArray[count] = EMPTY_HEART;
                }
            }
            //decrements current health
            currentHealth -= amount;
        }

        public static void IncrementHealth(float amount)
        {
            int count = 0;
            //reduces amount until incrementing will not result in more than the max health
            while (currentHealth + amount > maxHealth) {
                amount -= 0.5f;
            }
            //finds the last index of a half/full heart
            while (healthArray[count] > EMPTY_HEART)
            {
                count++;
            }
            count--;
            //increments the healthArray according on the amount and the last heart in the array
            if (amount == 0.5f)
            {
                if (healthArray[count] == HALF_HEART)
                {
                    healthArray[count] = FULL_HEART;
                }
                else
                {
                    healthArray[count + 1] = HALF_HEART;
                }
            }
            else {
                if (healthArray[count] == HALF_HEART)
                {
                    healthArray[count] = FULL_HEART;
                    healthArray[count + 1] = HALF_HEART;

                }
                else {
                    healthArray[count + 1] = FULL_HEART;
                }
            }
            //increments the current health
            currentHealth += amount;
        }

        /// <summary>
        /// Updates Rupee count to count + "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of rupees</param>
        public static void UpdateRupeeCount(int amount)
        {
            rupeeCount += amount;
            int leftDigit = rupeeCount / 10;
            int rightDigit = rupeeCount % 10;

            rupeeDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            rupeeDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        /// <summary>
        /// Updates key count to count + "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of keys</param>
        public static void UpdateKeyCount(int amount)
        {
            keyCount += amount;
            int leftDigit = keyCount / 10;
            int rightDigit = keyCount % 10;
           
           
           
            keyDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            keyDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }

        /// <summary>
        /// Updates bomb count to count + "amount" in HUD
        /// </summary>
        /// <param name="amount">New amount of bombs</param>
        public static void UpdateBombCount(int amount)
        {
            bombCount += amount;
            int leftDigit = bombCount / 10;
            int rightDigit = bombCount % 10;
            bombDigits[LeftDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(leftDigit.ToString());
            bombDigits[RightDigitIndex] = HUDSpriteFactoryInstance.CreateHUDSprite(rightDigit.ToString());
        }


        //makes the map visible
        public static void AddMap()
        {
            spriteAndPosList.Add(specialCaseDict["map"]);


        }

        //makes the triforce marker visible
        public static void AddTriforceMarker()
        {
            spriteAndPosList.Add(specialCaseDict["triforce"]);
        }

        //move the player marker depending on which room the player enters
        public static void UpdateMarker(Direction direction)
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
            switch (direction) {
                case Direction.North:
                    markerPos.Y = markerPos.Y - 4f;

                    break;
                case Direction.East:
                    markerPos.X = markerPos.X + 8f;
                    break;
                case Direction.South:
                    markerPos.Y = markerPos.Y + 4f;

                    break;
                case Direction.West:
                    markerPos.X = markerPos.X - 8f;

                    break;
                default:
                    break;

            
            }
            Tuple<ISprite, Vector2> adder = new Tuple<ISprite, Vector2>(posMarker, markerPos);
            spriteAndPosList.Add(adder);
        }

        public static void Update(GameTime gameTime)
        {
            
           
            foreach (var sprite in spriteAndPosList)
            {
                if (sprite.Equals(specialCaseDict["map"]))
                {
                    sprite.Item1.Update(gameTime);
                }
            }
            foreach (var sprite in spriteAndPosList)
            {
                if (!sprite.Equals(specialCaseDict["map"]))
                {
                    sprite.Item1.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Draw everything in HUD
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch spritebatch</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            
            foreach (var sprite in spriteAndPosList)
            {
                if (sprite.Equals(specialCaseDict["map"]))
                {
                    sprite.Item1.Draw(spriteBatch, sprite.Item2, SpriteEffects.None, Rotation, MapLayerDepth);
                }
            }
            foreach (var sprite in spriteAndPosList)
            {
                if (!sprite.Equals(specialCaseDict["map"]))
                {
                    sprite.Item1.Draw(spriteBatch, sprite.Item2, SpriteEffects.None, Rotation, AboveMapLayerDepth);
                }
            }

            DrawHealth(spriteBatch);

            for (int i = 0; i < 2; i++)
            {
                rupeeDigits[i].Draw(spriteBatch, positionDictionary[$"rupeePosition{i}"]);
                keyDigits[i].Draw(spriteBatch, positionDictionary[$"keyPosition{i}"]);
                bombDigits[i].Draw(spriteBatch, positionDictionary[$"bombPosition{i}"]);
            }
        }
    }
}

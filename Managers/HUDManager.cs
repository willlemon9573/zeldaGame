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
        const int MAX_ATTAINABLE_HEALTH = 13;
        private static List<Tuple<ISprite, Vector2>> healthList = new List<Tuple<ISprite, Vector2>>();
        private static Dictionary<String,Tuple<ISprite, Vector2>> specialCaseDict = new Dictionary<String, Tuple<ISprite, Vector2>>();
        const float STARTING_HEALTH = 6f;
        const float FULL_HEART = 1f;
        const float HALF_HEART = 0.5f;
        const float EMPTY_HEART = 0f;
        
        
        public static void Initialize()
        {

            Vector2 startingPos = new Vector2(180,40);
            float[] _hearts = new float[MAX_ATTAINABLE_HEALTH];
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

                if (!name.Contains("heart"))
                {
                    if (name.Contains("map") || name.Contains("triforce"))
                    {
                        specialCaseDict.Add(name,new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                    else
                    {
                        /* Add to List */
                        spriteAndPosList.Add(new Tuple<ISprite, Vector2>(HUDSprite, position));
                    }
                }
                

                /*Initialize hearts*/
                createHealth(startingPos); 
               
            }
        }

        public static void createHealth(Vector2 startingPos)
        {
            //creates the position for the first heart in the health bar
            Vector2 pos = startingPos;
            //creates the full heart sprites bbeing used
            ISprite fullHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("full_heart");
            //adds all the hearts to the list to late be updated and drawn
            for (int i = 0; i < STARTING_HEALTH; i++) {
                healthList.Add(new Tuple<ISprite, Vector2>(fullHeartSprite,pos));
                pos.X = pos.X + 9; 
            }
        }

        public static void decrementHearts(float amount, int health)
        {
            //creates sprites for different hearts that will be used
            ISprite halfHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("half_heart");
            ISprite emptyHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("empty_heart");
            //initializes things that will be used later
            Vector2 prevTemp = new Vector2(0, 0);
            Vector2 temp = new Vector2(0,0);
            ISprite prevTempSprite = halfHeartSprite;
            ISprite tempSprite = halfHeartSprite;
            float maxvalue = 0;
            float fltHealth = (float)health;
            if (fltHealth > 0f)
            {
                //finds the lats heart in heartlist that isnt an empty heart
                foreach (var sprite  in healthList) {
                    if (sprite.Item2.X > maxvalue && sprite.Item1 != emptyHeartSprite) {
                        prevTemp = temp;
                        prevTempSprite = tempSprite;
                        temp = sprite.Item2;
                        maxvalue = temp.X;
                        tempSprite = sprite.Item1;
                    }
               
                }
                //removes the last heart from the list
                Tuple<ISprite, Vector2> removerTup = new Tuple<ISprite, Vector2>(tempSprite, temp);
                Tuple<ISprite, Vector2> potentialRemoverTup = new Tuple<ISprite, Vector2>(prevTempSprite, prevTemp);
                healthList.Remove(removerTup);

                //adds a new heart depending on how much helath is being taken away
                if (tempSprite == halfHeartSprite && amount % 1 == 0.5)
                {

                    Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(emptyHeartSprite, temp);
                    healthList.Add(tempTup);

                    amount -= 0.5f;
                    //STARTING_HEALTH -= 0.5f;
                }
                else if (tempSprite == halfHeartSprite && amount % 1 != 0.5) {
                    healthList.Remove(potentialRemoverTup);

                    Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(emptyHeartSprite, temp);
                    Tuple<ISprite, Vector2> prevTempTup = new Tuple<ISprite, Vector2>(halfHeartSprite, prevTemp);
                    healthList.Add(prevTempTup);
                    healthList.Add(tempTup);
                }
                else
                {
                    if (amount % 1 == 0.5)
                    {
                        Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(halfHeartSprite, temp);
                        healthList.Add(tempTup);

                        amount -= 0.5f;
                    }
                    else
                    {
                        Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(emptyHeartSprite, temp);
                        healthList.Add(tempTup);

                        amount -= 1f;
                    }
                }
            }
            
            
            
           
        }

        public static void incrementHearts(float amount, int health)
        {
            ISprite halfHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("half_heart");
            ISprite emptyHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("empty_heart");
            ISprite fullHeartSprite = HUDSpriteFactory.Instance.CreateHUDSprite("full_heart");
            //initializes things that will be used later
            Vector2 prevTemp = new Vector2(0, 0);
            Vector2 temp = new Vector2(0, 0);
            ISprite prevTempSprite = halfHeartSprite;
            ISprite tempSprite = halfHeartSprite;

            float fltHealth = (float)health;
            float minvalue = 9999;

           
            //finds the lats heart in heartlist that isnt a full heart
            foreach (var sprite in healthList)
            {
                if (sprite.Item2.X < minvalue && sprite.Item1 != fullHeartSprite)
                {
                    prevTemp = temp;
                    prevTempSprite = tempSprite;
                    temp = sprite.Item2;
                    minvalue = temp.X;
                    tempSprite = sprite.Item1;
                }

            }
            //removes the first non full heart from the list
            Tuple<ISprite, Vector2> removerTup = new Tuple<ISprite, Vector2>(tempSprite, temp);
            Tuple<ISprite, Vector2> potentialRemoverTup = new Tuple<ISprite, Vector2>(prevTempSprite, prevTemp);
            healthList.Remove(removerTup);

            if (tempSprite == halfHeartSprite && amount % 1 == 0.5)
            {
                Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(fullHeartSprite, temp);
                healthList.Add(tempTup);
            }
            else if (tempSprite == halfHeartSprite && amount % 1 != 0.5) {
                Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(emptyHeartSprite, temp);
                Tuple<ISprite, Vector2> prevTempTup = new Tuple<ISprite, Vector2>(halfHeartSprite, prevTemp);
                healthList.Add(prevTempTup);
                healthList.Add(tempTup);
            }
            else
            {
                if (amount % 1 == 0.5)
                {
                    Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(halfHeartSprite, temp);
                    healthList.Add(tempTup);

                }
                else
                {
                    Tuple<ISprite, Vector2> tempTup = new Tuple<ISprite, Vector2>(fullHeartSprite, temp);
                    healthList.Add(tempTup);

                }
            }

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

        //makes the map visible
        public static void addMap() {
            spriteAndPosList.Add(specialCaseDict["map"]);

        }

        //makes the triforce marker visible
        public static void addTriforceMarker() {
            spriteAndPosList.Add(specialCaseDict["triforce"]);
        }

        //move the player marker depending on which room the player enters
        public static void updateMarker(int direction) {
           
            Vector2 markerPos = new Vector2(0,0);
            ISprite posMarker =  HUDSpriteFactory.Instance.CreateHUDSprite("player");
            foreach(var sprite in spriteAndPosList)
            {
                if (sprite.Item1 == posMarker) {
                    markerPos = sprite.Item2;
                }
            }
            Tuple<ISprite, Vector2> remover = new Tuple<ISprite, Vector2>(posMarker, markerPos);
            spriteAndPosList.Remove(remover);
            markerPos.X = markerPos.X + 5f;
        }

        public static void Update(GameTime gameTime) 
        {
              
            foreach (var sprite in healthList)
            {
                sprite.Item1.Update(gameTime);
            }
            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Update(gameTime);
            }

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
            foreach (var sprite in healthList)
            {
                sprite.Item1.Draw(spriteBatch, sprite.Item2);
            }
            foreach (var sprite in spriteAndPosList)
            {
                sprite.Item1.Draw(spriteBatch, sprite.Item2);
            }

        }
    }
}

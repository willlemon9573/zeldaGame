using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EntityInterfaces;
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
        private const string Map = "map";
        private const string Sprite = "Sprite";
        private const string Vector2 = "Vector2";
        private const string Triforce = "triforce";
        private const string Heart = "heart";
        private const string Name = "name";
        private const string NumPosition = "NumPosition";
        private const string Player = "player";
        private const string Zero = "0";
        private const int ItemDigits = 2;
        private const int LeftDigitIndex = 0; //array index 0
        private const int RightDigitIndex = 1; //array index 1
        private const float MapLayerDepth = 1f; // draw map on the layer depth that's considered "backgroud"
        private const float AboveMapLayerDepth = 0f; // draw any other markers on the layer depth that's considered 
        private const float Rotation = 0f; // because we need to add the layerdepth we also have to add rotation

        private static readonly List<Tuple<ISprite, Vector2>> spriteAndPosList = new List<Tuple<ISprite, Vector2>>();
        private static readonly Dictionary<string, Tuple<ISprite, Vector2>> _specialCaseDict = new Dictionary<string, Tuple<ISprite, Vector2>>();
        private static readonly SpriteEffects _spriteEffects = SpriteEffects.None;
        private static readonly Color DefaultColorMask = Color.White;
        private static readonly Dictionary<string, Vector2> positionDictionary = new Dictionary<string, Vector2>();
        public static HUDSpriteFactory HUDSpriteFactoryInstance = HUDSpriteFactory.Instance;
        private static readonly Dictionary<IEntity, (ISprite, Vector2)> _playerWeaponBox = new Dictionary<IEntity, (ISprite, Vector2)>();
        private static readonly Dictionary<IEntity, HPLinkedList> _playerHealthMap = new Dictionary<IEntity, HPLinkedList>();
        /* Tracking for stackable items */
        private static Dictionary<StackableItems, Action<int>> actionMap; // contains the actions for incrementing key, bomb and rupee count
        private static Dictionary<Direction, Vector2> _playerMarkerOffsetMap; // contains the offsets required for moving the square that represents the player on the map
        private static readonly List<ISprite> rupeeDigits = new List<ISprite>();
        private static readonly List<ISprite> keyDigits = new List<ISprite>();
        private static readonly List<ISprite> bombDigits = new List<ISprite>();




        /// <summary>
        /// Parses hud information to populate all the lists
        /// </summary>
        private static void ParseHUDXMLFile()
        {
            string path = @"XMLFiles/HUDXMLFiles/HUDPositions.xml";
            XDocument document = XDocument.Load(path);
            XElement root = document.Root; /* get root */
            XDocTools xDocTools = new XDocTools();

            foreach (XElement sprite in root.Elements(Sprite))
            {
                /* Get the sprite name */
                string name = xDocTools.ParseAttributeAsString(sprite.Attribute(Name));
                /* Get the position Element */
                XElement positionElement = sprite.Element(Vector2);
                /* Parse the Vector2 position Element */
                Vector2 position = xDocTools.ParseVector2Element(positionElement);
                /* Create Sprite */
                ISprite HUDSprite;
                if (name == Triforce)
                {
                    HUDSprite = HUDSpriteFactoryInstance.CreateAnimatedHUDSprite(name);
                }
                else
                {
                    HUDSprite = HUDSpriteFactoryInstance.CreateHUDSprite(name);
                }

                if (!name.Contains(Heart))
                {
                    if (name.Contains(Map) || name.Contains(Triforce))
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


            foreach (XElement numPosition in root.Elements(NumPosition))
            {
                /* Get the name */
                string name = xDocTools.ParseAttributeAsString(numPosition.Attribute(Name));
                /* Get the position Element */
                XElement positionElement = numPosition.Element(Vector2);
                /* Parse the Vector2 position Element */
                Vector2 position = xDocTools.ParseVector2Element(positionElement);
                positionDictionary.Add(name, position);
            }
        }

        /// <summary>
        /// Initialize lists and dictionaries needed for HUD by parsing
        /// </summary>
        public static void Initialize(List<IEntity> players)
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
                { StackableItems.Rupee, UpdateRupeeCount  },
                { StackableItems.Bomb, UpdateBombCount },
                { StackableItems.DungeonKey, UpdateKeyCount }
            };

            /* setup for moving marker when player changes rooms */
            Vector2 verticalOffset = new Vector2(4, 0);
            Vector2 horizontalOffset = new Vector2(0, 8);
            _playerMarkerOffsetMap = new Dictionary<Direction, Vector2>()
            {
                { Direction.North, -verticalOffset },
                { Direction.South, verticalOffset },
                { Direction.West, -horizontalOffset },
                { Direction.East, horizontalOffset },
            };

            /* set up linked list for hearts */
            Vector2 playerHeartStartingPosition = new Vector2(180, 40);
            Vector2 playerEquipmentBoxPosition = new Vector2(132, 35);
            int playerMaxStartingHealth = 3;
            int yOffset = 10;
            int xOffset = 25;
            foreach (IEntity player in players)
            {
                _playerHealthMap.Add(player,
                    new HPLinkedList(playerMaxStartingHealth, playerHeartStartingPosition, player));
                _playerWeaponBox.Add(player, (null, playerEquipmentBoxPosition));
                playerHeartStartingPosition.Y += yOffset;
                playerEquipmentBoxPosition.X += xOffset;
            }
        }

        /// <summary>
        /// For updating the on screen player equipment that is displayed
        /// </summary>
        /// <param name="weaponSprite">The sprite to be displayed</param>
        /// <param name="index">The index where the sprite is placed. 0 for left, 1 for right</param>
        public static void UpdateOnScreenEquipment(IEntity player, ISprite weaponSprite)
        {
            Vector2 pos = _playerWeaponBox[player].Item2;
            _playerWeaponBox[player] = (weaponSprite, pos);
        }

        /// <summary>
        /// Increases onscreen health by one
        /// </summary>
        public static void IncreasePlayerHealth()
        {
            foreach (HPLinkedList playerHealth in _playerHealthMap.Values)
            {
                playerHealth.IncreasePlayerHealth();
            }
        }

        /// <summary>
        /// Decrement the player health by the given amount
        /// </summary>
        /// <param name="amount">The amount to decrement player health</param>
        public static void DecrementHealth(IEntity player, float amount)
        {
            if (_playerHealthMap.TryGetValue(player, out HPLinkedList playerHP))
            {
                playerHP.DecrementCurrentHealth(amount);
            }
        }

        /// <summary>
        /// Increment's the player's health
        /// </summary>
        /// <param name="amount"></param>
        /// 
        public static void IncrementHearts(IEntity player, float amount)
        {
            if (_playerHealthMap.TryGetValue(player, out HPLinkedList playerHP))
            {
                playerHP.IncrementCurrentHealth(amount);
            }
        }

        /// <summary>
        /// Updates Rupee count to count + "amount" in HUD
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
        /// Updates key count to count + "amount" in HUD
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
        /// Updates bomb count to count + "amount" in HUD
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
            spriteAndPosList.Add(_specialCaseDict[Map]);
        }

        //makes the triforce marker visible
        public static void AddTriforceMarker()
        {
            spriteAndPosList.Add(_specialCaseDict[Triforce]);
        }

        //move the player marker depending on which room the player enters
        public static void UpdateMarker(Direction direction)
        {

            Vector2 markerPos = new Vector2(0, 0);
            ISprite posMarker = HUDSpriteFactory.Instance.CreateHUDSprite(Player);
            foreach (var sprite in spriteAndPosList)
            {
                if (sprite.Item1 == posMarker)
                {
                    markerPos = sprite.Item2;
                }
            }

            Tuple<ISprite, Vector2> remover = new Tuple<ISprite, Vector2>(posMarker, markerPos);
            spriteAndPosList.Remove(remover);
            markerPos += _playerMarkerOffsetMap[direction];
            Tuple<ISprite, Vector2> adder = new Tuple<ISprite, Vector2>(posMarker, markerPos);
            spriteAndPosList.Add(adder);
        }

        public static void Update(GameTime gameTime)
        {
            spriteAndPosList.ForEach(sprite => sprite.Item1.Update(gameTime));
        }

        /// <summary>
        /// Draw everything in HUD
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch spritebatch</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (HPLinkedList playerHealth in _playerHealthMap.Values)
            {
                playerHealth.Draw(spriteBatch);
            }

            foreach (var spriteTuple in _playerWeaponBox.Values)
            {
                ISprite sprite = spriteTuple.Item1;
                Vector2 position = spriteTuple.Item2;
                if (sprite != null)
                {
                    sprite.Draw(spriteBatch, position, DefaultColorMask);
                }
            }
            float layerDepth = 0f;
            foreach (var sprite in spriteAndPosList)
            {
                layerDepth = (sprite.Equals(_specialCaseDict[Map])) ? MapLayerDepth : AboveMapLayerDepth;
                sprite.Item1.Draw(spriteBatch, sprite.Item2, DefaultColorMask, _spriteEffects, Rotation, layerDepth);
            }
            for (int i = 0; i < ItemDigits; i++)
            {
                rupeeDigits[i].Draw(spriteBatch, positionDictionary[$"rupeePosition{i}"], DefaultColorMask);
                keyDigits[i].Draw(spriteBatch, positionDictionary[$"keyPosition{i}"], DefaultColorMask);
                bombDigits[i].Draw(spriteBatch, positionDictionary[$"bombPosition{i}"], DefaultColorMask);
            }
        }

        public static void Reset()
        {
            rupeeDigits.Clear();
            keyDigits.Clear();
            bombDigits.Clear();
            spriteAndPosList.Clear();
            _playerHealthMap.Clear();
            _playerMarkerOffsetMap.Clear();
            _specialCaseDict.Clear();
            positionDictionary.Clear();
        }
    }
}

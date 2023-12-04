using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.GameStateMenu
{
    /// <summary>
    /// Represents the item selection menu in the game.
    /// This class extends GameStateAbstract and manages the display and interaction within the item selection menu.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class ItemSelectionMenu : GameStateAbstract
    {
        private double elapsedTime = 0;
        private double interval = 0.2; // 0.2 seconds for toggling
        private bool toggle = false; // Toggle between true and false for selection

        // Constants for UI dimensions and styling
        private const int item_width = 16;
        private const int item_height = 16;
        private const int WidthPADDING = 4;
        private const int HeightPADDING = 2;
        private const float SCALE = 1f;
        private const float ROTATION = 0f;
        private const float LAYER_DEPTH = 0.01f;
        private const float OVER_LAYER_DEPTH = 0.1f;
        private const int BACKGROUNDHEIGH = 88;
        private const int INTERVAL_BETWEEN_BACKGROUND = 30;
        private const int ROWS = 1;

        // Rectangles for UI elements
        private Rectangle ChooseRectFir;
        private Rectangle ChooseRectSec;
        private Rectangle ChooseRect;
        private Rectangle currentWeaponRec;
        private Rectangle topBackGround;
        private Rectangle botBackGround;
        private Rectangle Cover;
        private Rectangle currentPlayer;

        // Textures
        private readonly Texture2D largeTexture;
        private readonly Texture2D itemChooseScreen;

        // Equipment and selection data
        private Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>> equipmentData;
        private Dictionary<DungeonItems, Tuple<Rectangle, Vector2>> _dungeonItemsData;
        private List<DungeonItems> _dungeonItems;
        private List<EquipmentItem> _playerEquipment;
        private EquipmentItem currentWeapon;
        private Vector2 currentWeaponPosition = new Vector2(63, 48);
        private Vector2 mapPosition = new Vector2(48, 140);
        private Vector2 compassPosition = new Vector2(45, 180);
        private Vector2 ChooseRecPosition;

        // Entity references
        private readonly IEntity _player;

        // Property for current weapon
        public EquipmentItem CurrentWeapon { get { return currentWeapon; } }

        private Dictionary<string, bool> _whetherVisitedRoom;
        private Dictionary<string, (Vector2 Position, bool Visited)> _roomsInfo;
        private string _playerCurrentRoom;


        /// <summary>
        /// Constructor for the ItemSelectionMenu. Initializes textures, fonts, and loads item data.
        /// </summary>
        /// <param name="game">Reference to the game object for accessing content.</param>
        /// <param name="Player">Reference to the player entity.</param>
        public ItemSelectionMenu(Game1 game, IEntity Player) : base(game)
        {
            _player = Player;
            _overlay.SetData(new[] { Color.Black });
            largeTexture = Texture2DManager.GetItemSpriteSheet();
            itemChooseScreen = Texture2DManager.GetPauseScreenSheet();
            _font = Texture2DManager.GetSpriteFont("PauseSetting");
            #region Equipment Data Initialization
            LoadItemData();
            #endregion
            _dungeonItems = PlayerInventoryManager.GetPlayerDungeonItems(_player);
            _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
            currentWeapon = _playerEquipment.FirstOrDefault();
            ChooseRect = ChooseRectFir;
            _whetherVisitedRoom = LevelManager.WhetherVisitedRoom;
            _roomsInfo = new Dictionary<string, (Vector2 Position, bool Visited)>();

        }
        /// <summary>
        /// Get which place the cover should be drawn
        /// </summary>
        /// <param name="roomName">Which room we are trying to get our Room Position</param>
        public Vector2 GetRoomPositionFromXml(string roomName)
        {
            XDocument doc = XDocument.Load(@"GameStateMenu/ItemData.xml");
            XDocTools _xDocTools = new XDocTools();
            XElement roomElement = doc.Descendants("Room").FirstOrDefault(room => room.Attribute("name").Value == roomName);

            if (roomElement != null)
            {
                Vector2 roomPosition = _xDocTools.ParseVector2Element(roomElement);
                return roomPosition;
            }
            else
            {
                //just set to an unsable and unrelated place
                Debug.WriteLine(roomName);
                return new Vector2(110, 140);
            }
        }

        /// <summary>
        /// Creates map and compass items for a dungeon level and updates their positions.
        /// </summary>
        /// <param name="itemDataElement">XML element containing dungeon item data.</param>
        /// <param name="_xDocTools">Toolset for XML document parsing and manipulation.</param>
        private void CreateMapOrCompass(XElement itemDataElement, XDocTools _xDocTools)
        {
            foreach (XElement itemElement in itemDataElement.Elements("DungeonItems"))
            {
                Rectangle itemRec = _xDocTools.CreateRectangle(itemElement);
                DungeonItems dungeonItems = _xDocTools.ParseAttributeADungeonItemsData(itemElement, "name");
                Debug.WriteLine(dungeonItems);
                Vector2 position = new Vector2(0, 0);
                _dungeonItemsData.Add(dungeonItems, new Tuple<Rectangle, Vector2>(itemRec, position));
            }
            var currentMapTuple = _dungeonItemsData[DungeonItems.Level1Map];
            var newMapTuple = new Tuple<Rectangle, Vector2>(currentMapTuple.Item1, mapPosition);
            _dungeonItemsData[DungeonItems.Level1Map] = newMapTuple;

            var currentCompassTuple = _dungeonItemsData[DungeonItems.Level1Compass];
            var newCompassTuple = new Tuple<Rectangle, Vector2>(currentCompassTuple.Item1, compassPosition);
            _dungeonItemsData[DungeonItems.Level1Compass] = newCompassTuple;
        }
        /// <summary>
        /// Loads item data from an XML file into the equipmentData dictionary.
        /// </summary>
        private void LoadItemData()
        {
            equipmentData = new Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>>();
            _dungeonItemsData = new Dictionary<DungeonItems, Tuple<Rectangle, Vector2>>();
            XDocument doc = XDocument.Load(@"GameStateMenu/ItemData.xml");
            var itemDataElement = doc.Element("ItemData");
            XDocTools _xDocTools = new XDocTools();
            XElement ChooseRecFirRectElement = itemDataElement.Element("ChooseRecFirSprite");
            ChooseRectFir = _xDocTools.CreateRectangle(ChooseRecFirRectElement);
            XElement ChooseRecSecRectElement = itemDataElement.Element("ChooseRecSecSprite");
            ChooseRectSec = _xDocTools.CreateRectangle(ChooseRecSecRectElement);
            XElement topBackGroundElement = itemDataElement.Element("BackGroundFirst");
            topBackGround = _xDocTools.CreateRectangle(topBackGroundElement);
            XElement botBackGroundElement = itemDataElement.Element("BackGroundSecond");
            botBackGround = _xDocTools.CreateRectangle(botBackGroundElement);
            XElement coverElement = itemDataElement.Element("Cover");
            Cover = _xDocTools.CreateRectangle(coverElement);
            XElement _currentPlayer = itemDataElement.Element("currentPlayer");
            currentPlayer = _xDocTools.CreateRectangle(_currentPlayer);

            CreateMapOrCompass(itemDataElement, _xDocTools);
            int boxX = 120;
            int boxY = 47;
            int boxWidth = 90;
            int boxHeight = 33;

            int itemsPerRow = 4;
            int startX = boxX + (boxWidth - (itemsPerRow * item_width + (itemsPerRow - 1) * HeightPADDING)) / 2;
            int x = startX;
            int y = boxY + (boxHeight / ROWS - item_height) / 2;

            int count = 0;

            foreach (XElement itemElement in itemDataElement.Elements("Item"))
            {
                Rectangle itemRec = _xDocTools.CreateRectangle(itemElement);
                EquipmentItem equipmentItem = _xDocTools.ParseAttributeAsEquipmentItem(itemElement, "name");

                if (count > 0)
                {
                    x += item_width + WidthPADDING;
                }
                count++;

                Vector2 position = new Vector2(x, y);
                equipmentData.Add(equipmentItem, new Tuple<Rectangle, Vector2>(itemRec, position));
            }

            foreach (XElement betterItemElement in itemDataElement.Elements("BetterItem"))
            {
                EquipmentItem betterEquipmentItem = _xDocTools.ParseAttributeAsEquipmentItem(betterItemElement, "name");

                string baseItemName = betterEquipmentItem.ToString().Replace("Better", "");

                EquipmentItem baseEquipmentItem = equipmentData.Keys
                    .SingleOrDefault(item => item.ToString() == baseItemName);

                if (!baseEquipmentItem.Equals(default(EquipmentItem)) && !baseEquipmentItem.Equals(EquipmentItem.WoodenSword))
                {
                    Rectangle betterItemRec = _xDocTools.CreateRectangle(betterItemElement);
                    Vector2 basePosition = equipmentData[baseEquipmentItem].Item2;
                    equipmentData.Add(betterEquipmentItem, new Tuple<Rectangle, Vector2>(betterItemRec, basePosition));
                }
            }

        }

        /// <summary>
        /// Updates the state of the item selection menu.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            UpdateChooseRecPosition();
            SetCurrentWeaponRec();
            AnimateChooseRec(gameTime);
        }

        /// <summary>
        /// Sets the next weapon in the equipment list as the current weapon.
        /// </summary>
        public void SetNextWeapon()
        {
            Debug.WriteLine("SetNextWeapon");
            int currentIndex = _playerEquipment.IndexOf(currentWeapon);
            if (_playerEquipment.Count != 0)
            {
                int nextIndex = (currentIndex + 1) % _playerEquipment.Count;
                EquipmentItem nextWeapon = _playerEquipment[nextIndex];
                currentWeapon = nextWeapon;
            }
        }

        /// <summary>
        /// Sets the previous weapon in the equipment list as the current weapon.
        /// </summary>
        public void SetPreviousWeapon()
        {
            int currentIndex = _playerEquipment.IndexOf(currentWeapon);
            if (_playerEquipment.Count != 0)
            {
                int PreviousIndex = (currentIndex - 1 + _playerEquipment.Count) % _playerEquipment.Count;
                EquipmentItem PreviousWeapon = _playerEquipment[PreviousIndex];
                currentWeapon = PreviousWeapon;
            }
        }

        /// <summary>
        /// Sets the current weapon's Rectangle based on the current weapon.
        /// </summary>
        private void SetCurrentWeaponRec()
        {
            if (currentWeapon == EquipmentItem.WoodenSword) return; // Return early if currentWeapon is null

            if (equipmentData.TryGetValue(currentWeapon, out Tuple<Rectangle, Vector2> equipmentInfo))
            {
                currentWeaponRec = equipmentInfo.Item1;
            }
        }

        /// <summary>
        /// Synchronizes the inventory with the player's current equipment.
        /// </summary>
        public void SynchronizeInventory()
        { 
            _whetherVisitedRoom = LevelManager.WhetherVisitedRoom;
            foreach (var roomEntry in _whetherVisitedRoom)
            {
                string roomName = roomEntry.Key;
                bool visited = roomEntry.Value;
                Vector2 position = GetRoomPositionFromXml(roomName); 
                _roomsInfo[roomName] = (position, visited);
            }

            if (currentWeapon == EquipmentItem.WoodenSword)
            {
                _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
                currentWeapon = _playerEquipment.FirstOrDefault();
            }
            else
            {
                _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
            }
            SynchronizeDungeonItems();
        }

        /// <summary>
        /// Synchronizes list of dungeon items owned by the player
        /// </summary>
        public void SynchronizeDungeonItems()
        {
            _dungeonItems = PlayerInventoryManager.GetPlayerDungeonItems(_player);
            if (_dungeonItems.Contains(DungeonItems.Level1Map))
            {
                var keys = new List<string>(_roomsInfo.Keys);

                foreach (var key in keys)
                {
                    var (position, _) = _roomsInfo[key];
                    _roomsInfo[key] = (position, true);
                }
            }
            _playerCurrentRoom = LevelManager.PlayerCurrentRoom;

        }

        /// <summary>
        /// Animates the selection rectangle by toggling its appearance over time.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values for animation.</param>
        private void AnimateChooseRec(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the elapsed time is greater than the interval
            if (elapsedTime >= interval)
            {
                // Reset the elapsed time
                elapsedTime = 0;
                // Toggle the state
                toggle = !toggle;
                // Change the ChooseRect based on the toggle state
                ChooseRect = toggle ? ChooseRectSec : ChooseRectFir;
            }
        }

        /// <summary>
        /// Updates the position of the selection rectangle based on the current weapon.
        /// </summary>
        private void UpdateChooseRecPosition()
        {
            if (currentWeapon == EquipmentItem.WoodenSword) return;
            if (equipmentData.TryGetValue(currentWeapon, out Tuple<Rectangle, Vector2> data))
            {
                int chooseRectWidth = ChooseRect.Width;

                // If found, return the Vector2, but adjust for the centering based on the width of the item
                Vector2 itemPosition = data.Item2;
                int itemWidth = data.Item1.Width;

                // Calculate the offset to center the ChooseRect around the item
                float offsetX = (chooseRectWidth - itemWidth) / 2f;

                // Adjust the position to center the ChooseRect around the item
                ChooseRecPosition = new Vector2(itemPosition.X - offsetX, itemPosition.Y);
            }
            else
            {
                var availableKeys = string.Join(", ", equipmentData.Keys.Select(k => k.ToString()));
                System.Diagnostics.Debug.WriteLine($"Available keys: {availableKeys}");
                System.Diagnostics.Debug.WriteLine($"Current weapon: {currentWeapon}");
                throw new KeyNotFoundException($"The currentWeapon '{currentWeapon}' does not exist in the equipment data. Available keys: {availableKeys}");
            }
        }

        /// <summary>
        /// Draws the item selection menu and its components.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, OVER_LAYER_DEPTH);
            DrawRoomCovers(spriteBatch);
            DrawPlayerPosition(spriteBatch);
            DrawbackGround(spriteBatch);
            DrawDifferentItem(spriteBatch);
            DrawDifferentDungeonItems(spriteBatch);
            if (currentWeapon == EquipmentItem.WoodenSword) return;
            spriteBatch.Draw(largeTexture, currentWeaponPosition, currentWeaponRec, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
            DrawChooseRec(spriteBatch);

        }

        /// <summary>
        /// Draws the selection rectangle.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        private void DrawChooseRec(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemChooseScreen, ChooseRecPosition, ChooseRect, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws the background of the item selection menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        private void DrawbackGround(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                itemChooseScreen,
                destinationRectangle: new Rectangle(0, 0, WIDTH, BACKGROUNDHEIGH),
                sourceRectangle: topBackGround,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0.05f);

            spriteBatch.Draw(
                itemChooseScreen,
                destinationRectangle: new Rectangle(0, BACKGROUNDHEIGH + INTERVAL_BETWEEN_BACKGROUND, WIDTH, BACKGROUNDHEIGH),
                sourceRectangle: botBackGround,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0.05f);
        }

        /// <summary>
        /// Draws different items in the item selection menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        private void DrawDifferentItem(SpriteBatch spriteBatch)
        {

            foreach (var equipmentItem in _playerEquipment)
            {
                if (equipmentData.TryGetValue(equipmentItem, out Tuple<Rectangle, Vector2> itemData))
                {
                    // Retrieve the source rectangle and position from the tuple
                    Rectangle sourceRect = itemData.Item1;
                    Vector2 position = itemData.Item2;

                    // Draw the item using the retrieved source rectangle and position
                    spriteBatch.Draw(largeTexture, position, sourceRect, Color.White, ROTATION, Vector2.Zero, SCALE, SpriteEffects.None, LAYER_DEPTH);
                }
            }
        }

        /// <summary>
        /// Draws different items in the item selection menu.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        private void DrawDifferentDungeonItems(SpriteBatch spriteBatch)
        {

            foreach (var dungeonItem in _dungeonItems)
            {
                if (_dungeonItemsData.TryGetValue(dungeonItem, out Tuple<Rectangle, Vector2> itemData))
                {
                    // Retrieve the source rectangle and position from the tuple
                    Rectangle sourceRect = itemData.Item1;
                    Vector2 position = itemData.Item2;

                    // Draw the item using the retrieved source rectangle and position
                    spriteBatch.Draw(itemChooseScreen, position, sourceRect, Color.White, ROTATION, Vector2.Zero, SCALE, SpriteEffects.None, LAYER_DEPTH);
                }
            }
        }

        /// <summary>
        /// Draws the player's current position on the screen using the sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used to draw the player's position.</param>
        public void DrawPlayerPosition(SpriteBatch spriteBatch)
        {
            // Declare the variable to store the current player position
            Vector2 currentPlayerPosition;

            // Check if the _roomsInfo dictionary contains the key _playerCurrentRoom
            if (_roomsInfo.ContainsKey(_playerCurrentRoom))
            {
                // Extract the Position part of the tuple from the _roomsInfo dictionary
                currentPlayerPosition = _roomsInfo[_playerCurrentRoom].Position;
            }
            else
            {
                // Handle the case where _playerCurrentRoom is not a key in _roomsInfo
                // For example, you might set currentPlayerPosition to a default value or throw an exception
                currentPlayerPosition = new Vector2(0, 0); // or any default position
            }
            spriteBatch.Draw(
                        itemChooseScreen,
                        currentPlayerPosition,
                        currentPlayer,
                        Color.White,
                        0,
                        Vector2.Zero,
                        SCALE,
                        SpriteEffects.None,
                        0
                        );
        }

        /// <summary>
        /// Draws covers over rooms that have not been visited yet, using the sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing room covers.</param>
        public void DrawRoomCovers(SpriteBatch spriteBatch)
        {
            foreach (var roomInfo in _roomsInfo)
            {
                if (!roomInfo.Value.Visited)
                {
                    Vector2 position = roomInfo.Value.Position;
                    Debug.WriteLine(position);
                    spriteBatch.Draw(
                        itemChooseScreen, 
                        position, 
                        Cover, 
                        Color.White, 
                        0, 
                        Vector2.Zero, 
                        SCALE, 
                        SpriteEffects.None, 
                        0 
                    );
                }
            }
        }
    }
}
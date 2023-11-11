using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SprintZero1.Managers;
using SprintZero1.Enums;
using SprintZero1.Entities;
using System;
using System.Linq;
using System.Xml.Linq;
using SprintZero1.InventoryFiles;
using SprintZero1.XMLParsers;
using System.Diagnostics;

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
        private double interval = 0.3; // 0.3 seconds for toggling
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

        // Textures
        private Texture2D largeTexture;
        private Texture2D itemChooseScreen;

        // Equipment and selection data
        private Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>> equipmentData;
        private List<EquipmentItem> _playerEquipment;
        private EquipmentItem currentWeapon;
        private Vector2 currentWeaponPosition = new Vector2(63, 48);
        private Vector2 ChooseRecPosition;

        // Entity references
        private readonly IEntity _player;

        // Property for current weapon
        public EquipmentItem CurrentWeapon { get { return currentWeapon; } }


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
            _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
            currentWeapon = _playerEquipment.FirstOrDefault();
            ChooseRect = ChooseRectFir;
        }

        /// <summary>
        /// Loads item data from an XML file into the equipmentData dictionary.
        /// </summary>
        private void LoadItemData()
        {
            equipmentData = new Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>>();
            XDocument doc = XDocument.Load("ItemData.xml");
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
                Debug.WriteLine(itemElement);
                Rectangle itemRec = _xDocTools.CreateRectangle(itemElement);
                EquipmentItem equipmentItem = _xDocTools.ParseAttributeAsEquipmentItem(itemElement, "name");

                Debug.WriteLine(equipmentItem);

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
            updateChooseRecPosition();
            SetCurrentWeaponRec();
            AnimateChooseRec(gameTime);
        }

        /// <summary>
        /// Sets the next weapon in the equipment list as the current weapon.
        /// </summary>
        public void SetNextWeapon()
        {
            int currentIndex = _playerEquipment.IndexOf(currentWeapon);
            int nextIndex = (currentIndex + 1) % _playerEquipment.Count;
            EquipmentItem nextWeapon = _playerEquipment[nextIndex];
            currentWeapon = nextWeapon;
        }

        /// <summary>
        /// Sets the previous weapon in the equipment list as the current weapon.
        /// </summary>
        public void SetPreviousWeapon()
        {
            int currentIndex = _playerEquipment.IndexOf(currentWeapon);
            int PreviousIndex = (currentIndex - 1 + _playerEquipment.Count) % _playerEquipment.Count;
            EquipmentItem PreviousWeapon = _playerEquipment[PreviousIndex];
            currentWeapon = PreviousWeapon;
        }

        /// <summary>
        /// Sets the current weapon's Rectangle based on the current weapon.
        /// </summary>
        private void SetCurrentWeaponRec()
        {
            if (currentWeapon == null) return; // Return early if currentWeapon is null

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
            if (currentWeapon == null)
            {
                _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
                currentWeapon = _playerEquipment.FirstOrDefault();
            }
            else
            {
                _playerEquipment = PlayerInventoryManager.GetPlayerEquipmentList(_player);
            }
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
        private void updateChooseRecPosition()
        {
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
            DrawbackGround(spriteBatch);
            DrawDifferentItem(spriteBatch);
            if (currentWeapon == null) return;
            spriteBatch.Draw(largeTexture, currentWeaponPosition, currentWeaponRec, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
            DrawChooseRec(spriteBatch);

        }

        /// <summary>
        /// Draws the selection rectangle.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing.</param>
        private void DrawChooseRec(SpriteBatch spriteBatch)
        {
            Debug.WriteLine(ChooseRecPosition);
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
    }
}
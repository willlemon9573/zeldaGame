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

namespace SprintZero1.GameStateMenu
{
    internal class ItemSelectionMenu : GameStateAbstract
    {
        private double elapsedTime = 0;
        private double interval = 0.5; // 0.5 seconds
        private bool toggle = false; // This will toggle between true and false
        private const int item_width = 16;
        private const int item_height = 16;
        private const int PADDING = 10;
        private const float SCALE = 1f;
        private const float ROTATION = 0f;
        private const float LAYER_DEPTH = 0.01f;
        private const float OVER_LAYER_DEPTH = 0.02f;
        private const int ROWS = 2;
        private readonly PlayerInventory _playerInventory;
        private  Rectangle ChooseRectFir;
        private  Rectangle ChooseRectSec;
        private Rectangle ChooseRect;
        private Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>> equipmentData;
        //private readonly ICombatEntity _player; //not sure whether need _player or not
        private  Texture2D largeTexture;
        private  Texture2D itemChooseScrene;
        private List<EquipmentItem> _playerEquipment;
        private EquipmentItem currentWeapon;
        private Vector2 ChooseRecPosition;

        private void LoadItemData()
        {

            XDocument doc = XDocument.Load("ItemData.xml");
            var itemDataElement = doc.Element("ItemData");
            XDocTools _xDocTools = new XDocTools();
            XElement ChooseRecFirRectElement = itemDataElement.Element("ChooseRecFirSprite");
            ChooseRectFir = _xDocTools.CreateRectangle(ChooseRecFirRectElement);
            XElement ChooseRecSecRectElement = itemDataElement.Element("ChooseRecSecSprite");
            ChooseRectSec = _xDocTools.CreateRectangle(ChooseRecSecRectElement);

            int itemCount = equipmentData.Count;
            int itemsPerRow = (int)Math.Ceiling(itemCount / (double)ROWS);
            int totalWidth = itemsPerRow * (item_width + PADDING) - PADDING;
            int totalHeight = ROWS * (item_height + PADDING) - PADDING;

            int startX = (WIDTH - totalWidth) / 2;
            int startY = (HEIGHT - totalHeight) / 2;

            int x = startX;
            int y = startY;

            int count = 0;

            foreach (XElement itemElement in itemDataElement.Elements("Item"))
            {
                Rectangle itemRec = _xDocTools.CreateRectangle(itemElement);
                EquipmentItem equipmentItem = _xDocTools.ParseAttributeAsEquipmentItem(itemElement, "name");

                Vector2 position = new Vector2(x, y);
                equipmentData[equipmentItem] = new Tuple<Rectangle, Vector2>(itemRec, position);

                x += item_width + PADDING;
                count++;

                if (count == itemsPerRow)
                {
                    count = 0;
                    x = startX;
                    y += item_height + PADDING;
                }
            }
        }

        public ItemSelectionMenu(Game1 game, PlayerInventory playerInventory):base(game)
        {
            
            _overlay.SetData(new[] { Color.Blue });
            _playerInventory = playerInventory;
            largeTexture = Texture2DManager.GetItemSpriteSheet();
            itemChooseScrene = Texture2DManager.GetPauseScreneSheet();
            _font = Texture2DManager.GetSpriteFont("PauseSetting");
            #region Equipment Data Initialization
            equipmentData = new Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>>();
            LoadItemData();
            #endregion
            _playerEquipment = _playerInventory.GetEquipmentList();
            currentWeapon = _playerEquipment.First();
            ChooseRect = ChooseRectFir;


        }

        public override void Update(GameTime gameTime)
        {
            SynchronizeInventory();
            updateChooseRecPosition();
            updateChooseRecPosition();
        }

        private void SynchronizeInventory()
        {
            _playerEquipment = _playerInventory.GetEquipmentList();
        }

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
                throw new KeyNotFoundException("The currentWeapon does not exist in the equipment data.");
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, OVER_LAYER_DEPTH);
            DrawDifferentItem(spriteBatch);
            DrawChooseRec(spriteBatch);

        }

        private void DrawChooseRec(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemChooseScrene, ChooseRecPosition, ChooseRect, Color.White, ROTATION, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
        }

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
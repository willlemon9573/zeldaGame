using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SprintZero1.Managers;
using SprintZero1.Enums;
using SprintZero1.Entities;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.GameStateMenu
{
    internal class ItemSelectionMenu : GameStateAbstract
    {
        private const int item_width = 16;
        private const int item_height = 16;
        private const int PADDING = 10;
        private const float SCALE = 1f;
        private const float ROTATION = 0f;
        private const float LAYER_DEPTH = 0.01f;
        private const float OVER_LAYER_DEPTH = 0.02f;
        private const int ROWS = 2;
        private readonly PlayerInventory _playerInventory;
        private Rectangle heartSourceRect;
        private Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>> equipmentData;
        //private readonly ICombatEntity _player; //not sure whether need _player or not
        private readonly int _playerHealth;
        private readonly Texture2D largeTexture;

        private void LoadItemData()
        {
            XDocument doc = XDocument.Load("ItemData.xml");
            var itemDataElement = doc.Element("ItemData");
            XDocTools _xDocTools = new XDocTools();
            XElement heartSourceRectElement = itemDataElement.Element("HeartSourceRect");
            heartSourceRect = _xDocTools.CreateRectangle(heartSourceRectElement);

            int itemCount = equipmentData.Count;
            int itemsPerRow = (int)Math.Ceiling(itemCount / (double)ROWS);
            int totalWidth = itemsPerRow * (item_weight + PADDING) - PADDING;
            int totalHeight = ROWS * (item_height + PADDING) - PADDING;

            int startX = (WIDTH - totalWidth) / 2;
            int startY = (HEIGHT - totalHeight) / 2;

            int x = startX;
            int y = startY;

            int count = 0;

            foreach (XElement itemElement in itemsElement.Elements("Item"))
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

        public ItemSelectionMenu(Game1 game, PlayerInventory playerInventory, ICombatEntity player):base(game)
        {
            _overlay.SetData(new[] { Color.Blue });
            _playerInventory = playerInventory;
            _playerHealth = player.Health;
            largeTexture = Texture2DManager.GetItemSpriteSheet();
            _font = game.Content.Load<SpriteFont>("PauseSetting");
            #region Equipment Data Initialization
            equipmentData = new Dictionary<EquipmentItem, Tuple<Rectangle, Vector2>>();
            LoadItemData();
            #endregion



        }
        public override void Update(GameTime gameTime)
        {
           
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, OVER_LAYER_DEPTH);
            DrawHeart(spriteBatch);
            DrawDifferentItem(spriteBatch);


        }

        private void DrawHeart(SpriteBatch spriteBatch)
        {
            string heartCountText = "x" + _playerHealth;
            Vector2 screenCenter = new Vector2(WIDTH / 2, HEIGHT / 4);  
            Vector2 heartPosition = new Vector2(screenCenter.X - heartSourceRect.Width / 2, screenCenter.Y - heartSourceRect.Height / 2);
            Vector2 textPosition = new Vector2(heartPosition.X + heartSourceRect.Width, screenCenter.Y - _font.MeasureString(heartCountText).Y / 2);
            Rectangle destinationRect = new Rectangle((int)heartPosition.X, (int)heartPosition.Y, heartSourceRect.Width, heartSourceRect.Height);
            spriteBatch.Draw(largeTexture, destinationRect, heartSourceRect, Color.White, 0f, Vector2.Zero, SpriteEffects.None, LAYER_DEPTH);
            spriteBatch.DrawString(_font, heartCountText, textPosition, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, LAYER_DEPTH);
        }

        private void DrawDifferentItem(SpriteBatch spriteBatch)
        {

            int itemCount = equipmentData.Count;
            int itemsPerRow = (int)Math.Ceiling(itemCount / (double)ROWS);
            int totalWidth = itemsPerRow * (item_weight + PADDING) - PADDING;
            int totalHeight = ROWS * (item_height + PADDING) - PADDING;

            int startX = (WIDTH - totalWidth) / 2;
            int startY = (HEIGHT - totalHeight) / 2;

            int x = startX;
            int y = startY;

            int count = 0;

            foreach (var item in equipmentData)
            {
                EquipmentItem equipmentItem = item.Key;
                Rectangle sourceRect = item.Value.Item1;
                Vector2 position = new Vector2(x, y);

                if (_playerInventory.IsInInventory(equipmentItem))
                {
                    spriteBatch.Draw(largeTexture, position, sourceRect, Color.White, ROTATION, Vector2.Zero, SCALE, SpriteEffects.None, LAYER_DEPTH);
                }

                x += item_weight + PADDING;
                count++;

                if (count == itemsPerRow)
                {
                    count = 0;
                    x = startX;
                    y += item_height + PADDING;
                }
            }
        }

    }

}
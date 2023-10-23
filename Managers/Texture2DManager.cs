﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.Managers
{
    /// <summary>
    /// Texture manager used to return textures when needed
    /// </summary>
    public static class Texture2DManager
    {
        private static Texture2D _linkSpriteSheet;
        private static Texture2D _tileSpriteSheet;
        private static Texture2D _enemySpriteSheet;
        private static Texture2D _bossSpriteSheet;
        private static Texture2D _level1FloorSpriteSheet;
        private static Texture2D _itemSpriteSheet;
        private static Texture2D _weaponSpriteSheet;
        /* for update */
        //private static Dictionary<string, Texture2D> textureDictionary;

        /// <summary>
        /// Loads all the textures required to create sprites
        /// </summary>
        /// <param name="contentManager">The </param>
        public static void LoadAllTextures(ContentManager contentManager)
        {
            _linkSpriteSheet = contentManager.Load<Texture2D>("8366");
            _tileSpriteSheet = contentManager.Load<Texture2D>("TileSheet");
            _enemySpriteSheet = contentManager.Load<Texture2D>("DungeonEnemySpritesheet");
            _bossSpriteSheet = contentManager.Load<Texture2D>("BossSpriteSheet");
            _level1FloorSpriteSheet = contentManager.Load<Texture2D>("level1");
            _weaponSpriteSheet = contentManager.Load<Texture2D>("LinkSheet");
            _itemSpriteSheet = contentManager.Load<Texture2D>("itemSpriteSheet1");

            /* Code for refactoring Texture2DManager refactoring to use one function rather than making constant new ones */
            /* _textureDictionary = new Dictionary<string, Texture2D>() {
                { "link", contentManager.Load<Texture2D>("8366") },
                { "tiles", contentManager.Load<Texture2D>("TileSheet") },
                { "dung_enemies", contentManager.Load<Texture2D>("DungeonEnemySpritesheet") },
                { "boss_enemies", contentManager.Load<Texture2D>("BossSpriteSheet") },
                { "floors", contentManager.Load<Texture2D>("level1") }
            }; */
        }

        public static Texture2D GetLinkSpriteSheet()
        {
            return _linkSpriteSheet;
        }

        /// <summary>
        /// Get the sprite sheet that holds the tile sheet
        /// </summary>
        /// <returns>Returns the Texture2D object containing tiles</returns>
        public static Texture2D GetTileSheet() { return _tileSpriteSheet; }

        /// <summary>
        /// Get the sprite sheet that holds the Enemy sprites
        /// </summary>
        /// <returns>Texture2D object with Enemy Sprite content</returns>
        public static Texture2D GetEnemySpriteSheet() { return _enemySpriteSheet; }

        /// <summary>
        /// Get the sprite sheet that holds the Boss Sprites
        /// </summary>
        /// <returns></returns>
        public static Texture2D GetBossSpriteSheet() { return _bossSpriteSheet; }
        /// <summary>
        /// Get the sprite sheet required for level one, but will be replaced
        /// </summary>
        /// <returns>Texture2D object with level one sprite  content</returns>
        public static Texture2D GetLevelOneSpriteSheet() { return _level1FloorSpriteSheet; }
        /// <summary>
        /// Get the sprite sheet that holds Weapon Sprites for animation
        /// </summary>
        /// <returns></returns>
        public static Texture2D GetWeaponSpriteSheet() { return _weaponSpriteSheet; }
        /// <summary>
        /// Get the sprite sheet for lootable items
        /// </summary>
        /// <returns></returns>
        public static Texture2D GetItemSpriteSheet() { return _itemSpriteSheet; }
    }
}

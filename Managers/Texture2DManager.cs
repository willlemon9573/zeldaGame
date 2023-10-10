using Microsoft.Xna.Framework.Content;
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
        /// <returns></returns>
        public static Texture2D GetEnemySpriteSheet() { return _enemySpriteSheet; }

        /// <summary>
        /// Get the sprite sheet that holds the Boss Sprites
        /// </summary>
        /// <returns></returns>
        public static Texture2D GetBossSpriteSheet() { return _bossSpriteSheet; }
    }
}

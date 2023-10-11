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
        private static Texture2D _levelSpriteSheet;

        /// <summary>
        /// Loads all the textures required to create sprites
        /// </summary>
        /// <param name="contentManager">The </param>
        public static void LoadAllTextures(ContentManager contentManager)
        {
            _linkSpriteSheet = contentManager.Load<Texture2D>("8366");
            _levelSpriteSheet = contentManager.Load<Texture2D>("TileSheet");
        }

        public static Texture2D GetLinkSpriteSheet()
        {
            return _linkSpriteSheet;
        }

        public static Texture2D GetLevelSpriteSheet()
        {
            return _levelSpriteSheet;
        }


    }
}

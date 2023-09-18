using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class BlockFactory : IBlockFactory
    {
        private Texture2D blockSpriteSheet;
        /// <summary>
        /// SourceRectangles will contain the name of the block alongside the following values for the rectangle
        /// (x, y, width, height) where (x, y) is the origin of the block on the sprite sheet, width is the width of the sprite
        /// and height is the height of the sprite
        /// </summary>
        private Dictionary<string, Rectangle> sourceRectangles;
        private static readonly BlockFactory instance = new BlockFactory();


        /// <summary>
        /// BlockFactory is a singleton allowing access to call the Block Factory whenever needed without creating a new concrete object
        /// </summary>
        public static BlockFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private BlockFactory() { }

        public void Initialize()
        {
            sourceRectangles = new Dictionary<string, Rectangle>
            {
                { "flat", new Rectangle(984, 11, 16, 16) },
                { "pyramid", new Rectangle(1001, 11, 16, 16) },
                { "stairs", new Rectangle(1035, 28, 16, 16) },
                { "greybrick", new Rectangle(984, 45, 16, 16) }
            };
        }

        public void LoadTextures(ContentManager manager)
        {
            blockSpriteSheet = manager.Load<Texture2D>("TileSheet");
        }

        public ISprite CreateNonMovingBlockSprite(string blockName)
        {
            Debug.Assert(blockName != null, "blockName is null");
            Debug.Assert(sourceRectangles.ContainsKey(blockName), "Source Rectangle does not contain the block named: " + blockName);
            return new NonMovingBlockSprite(sourceRectangles[blockName], blockSpriteSheet);
        }
    }
}

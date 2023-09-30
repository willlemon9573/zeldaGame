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
        private readonly Dictionary<string, Rectangle> sourceRectangles;
        private static readonly BlockFactory instance = new BlockFactory();
        private readonly List<string> blockNamesList;

        /// <summary>
        /// BlockFactory is a singleton allowing access to call the Block Factory whenever needed without creating a new concrete object
        /// </summary>
        public static BlockFactory Instance
        {
            get { return instance; }
        }


        /// <summary>
        /// Block Factory Property to get the current block list
        /// </summary>
        public List<string> BlockNamesList
        {
            get { return blockNamesList; }
        }

        /// <summary>
        /// Creates a dictionary where the keys are related to the block names
        /// and each value is related to the source rectangle (coordinates and dimensions)
        /// found on the tile sheet
        /// </summary>
        private void CreateSourceRectanglesDictionary()
        {
            int x_pixels = 984, y_pixels = 11; // starting coordiantes of the tiles
            const int WIDTH = 16, HEIGHT = 16; // dimmension of each tile
            foreach (string blockName in BlockNamesList)
            {
                // Add tile name with coordinates and dimensions to the dictionary
                sourceRectangles.Add(blockName, new Rectangle(x_pixels, y_pixels, WIDTH, HEIGHT));
                x_pixels += 17; // move to next column in the current row of tiles
                // when x_pixels exceed 1035, reset the x_pixels and increment y_pixels to access the next row
                if (x_pixels > 1035)
                {
                    x_pixels = 984;
                    y_pixels += 17;
                }
            }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private BlockFactory()
        {
            blockNamesList = new List<string>()
            {
                "flat", "pyramid", "statue1", "statue2",
                "hole", "spackled", "blueflat", "stairs",
                "greybrick", "greystriped"
            };
            sourceRectangles = new Dictionary<string, Rectangle>();
            CreateSourceRectanglesDictionary();
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
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
        /// Creates a dictionary where each key is related to a block name
        /// and each value is the source rectangle ([x,y] pixel locations) related to the key 
        /// found on the tile sheet
        /// </summary>
        private void CreateBlockDictionary()
        {
            int[] columns = new int[] { 984, 1001, 1018, 1035 };
            int[] rows = new int[] { 11, 28, 45 }; 
            const int WIDTH = 16, HEIGHT = 16, ROWCOUNT = 4;
            int columnIndex = 0, rowIndex = 0; 

            foreach (string blockName in BlockNamesList) 
            {
                sourceRectangles.Add(blockName, new Rectangle(columns[columnIndex], rows[rowIndex], WIDTH, HEIGHT));
                columnIndex = (columnIndex + 1) % ROWCOUNT; // 4 blocks per row excluding the last row which has 2
                // 4 blocks per row, move to the next row when the final block of a row is added
                if (columnIndex == 0) { rowIndex++; }
            }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private BlockFactory() {
            blockNamesList = new List<string>()
            {
                "flat", "pyramid", "statue1", "statue2",
                "hole", "spackled", "blueflat", "stairs",
                "greybrick", "greystriped"
            };
            sourceRectangles = new Dictionary<string, Rectangle>();
            CreateBlockDictionary();
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

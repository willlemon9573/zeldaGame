using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class TileSpriteFactory
    {
        private Texture2D tileSpriteSheet;
        private readonly Dictionary<string, Rectangle> tileSourceRectangles;
        private readonly Dictionary<int, Rectangle> wallSourceRectangles;
        private static readonly TileSpriteFactory instance = new TileSpriteFactory();
        private readonly List<string> blockNamesList;

        /// <summary>
        /// BlockFactory is a singleton allowing access to call the Block Factory whenever needed without creating a new concrete object
        /// </summary>
        public static TileSpriteFactory Instance
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
        /// and each value is related to the source rectangle (spriteOrigin and dimensions)
        /// found on the tile sheet
        /// </summary>
        private void CreateTileSourceDictionary()
        {
            int x_pixels = 984, y_pixels = 11; // starting coordiantes of the tiles
            const int WIDTH = 16, HEIGHT = 16; // dimmension of each tile
            foreach (string blockName in BlockNamesList)
            {
                // Add tile name with spriteOrigin and dimensions to the dictionary
                tileSourceRectangles.Add(blockName, new Rectangle(x_pixels, y_pixels, WIDTH, HEIGHT));
                x_pixels += 17; // move to next column in the current row of tiles
                // when x_pixels exceed 1035, reset the x_pixels and increment y_pixels to access the next row
                if (x_pixels > 1035)
                {
                    x_pixels = 984;
                    y_pixels += 17;
                }
            }
        }

        private void CreateWallSourceDictionary()
        {
            const int WIDTH = 112, LENGTH = 72;
            const int QUAD_ONE = 1, QUAD_TWO = 2, QUAD_THREE = 3, QUAD_FOUR = 4;
            // used to set the offset of coordinates between each quadrant
            const int X_OFFSET = 144, Y_OFFSET = 104;
            Vector2 spriteOrigin = new Vector2(665, 11);
            // Top Right Wall (Quadrant 1)
            wallSourceRectangles.Add(QUAD_ONE, new Rectangle((int)spriteOrigin.X, (int)spriteOrigin.Y, WIDTH, LENGTH));
            // Top Left Wall (Quadrant 2);
            wallSourceRectangles.Add(QUAD_TWO, new Rectangle((int)spriteOrigin.X - X_OFFSET, (int)spriteOrigin.Y, WIDTH, LENGTH));
            // bottom left wall (Quadrant 3);
            wallSourceRectangles.Add(QUAD_THREE, new Rectangle((int)spriteOrigin.X - X_OFFSET, (int)spriteOrigin.Y + Y_OFFSET, WIDTH, LENGTH));
            // Bottom Right Wall (Quadrant 4)
            wallSourceRectangles.Add(QUAD_FOUR, new Rectangle((int)spriteOrigin.X, (int)spriteOrigin.Y + Y_OFFSET, WIDTH, LENGTH));
        }

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private TileSpriteFactory()
        {
            blockNamesList = new List<string>()
            {
                "flat", "pyramid", "statue1", "statue2",
                "hole", "spackled", "blueflat", "stairs",
                "greybrick", "greystriped"
            };
            tileSourceRectangles = new Dictionary<string, Rectangle>();
            wallSourceRectangles = new Dictionary<int, Rectangle>();
            CreateTileSourceDictionary();
            CreateWallSourceDictionary();
        }

        public void LoadTextures()
        {
            tileSpriteSheet = Texture2DManager.GetTileSheet();
        }

        public ISprite CreateNewTileSprite(string blockName)
        {
            Debug.Assert(blockName != null, "blockName is null");
            Debug.Assert(tileSourceRectangles.ContainsKey(blockName), "Source Rectangle does not contain the block named: " + blockName);
            return new NonAnimatedSprite(tileSourceRectangles[blockName], tileSpriteSheet);
        }

        public ISprite CreateNewWallSprite(int quadrant)
        {
            Debug.Assert(wallSourceRectangles.ContainsKey(quadrant), "Incorrect Quadrant: " + quadrant);
            Debug.WriteLine(wallSourceRectangles[quadrant]);
            return new NonAnimatedSprite(wallSourceRectangles[quadrant], tileSpriteSheet);
        }
    }
}
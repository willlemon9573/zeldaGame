using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    public class TileSpriteFactory
    {
        private Texture2D tileSpriteSheet;
        private Texture2D levelOneSpriteSheet;
        private readonly Dictionary<string, Rectangle> tileSourceRectangles;
        private readonly Dictionary<int, Rectangle> wallSourceRectangles;
        private readonly Dictionary<String, Rectangle> levelOneSourceRectangles;
        private static readonly TileSpriteFactory instance = new TileSpriteFactory();

        /// <summary>
        /// Tile Factory is a singleton allowing access to call the Tile Factory whenever needed without creating a new concrete object
        /// </summary>
        public static TileSpriteFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Tile Factory Property to get the current tile list
        /// </summary>


        /// <summary>
        /// Adds the coordinates of the walls from the sprite sheet into the dictionary
        /// </summary>
        private void AddWallSourceRectangles()
        {
            //TODO: Create an XML File to parse - In discussion with Will/Jarek on how we want to handle walls
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
            /* should be deleted, but saving this for when we refactor */
            /*foreach (var kvp in wallSourceRectangles)
            {
                Debug.WriteLine("<Sprite quadrant=\"" + kvp.Key + "\">");
                Debug.WriteLine("\t<Rectangle x='" + kvp.Value.X + "' y='" + kvp.Value.Y + "' width='" + WIDTH + "' height='" + LENGTH + "'/>");
                Debug.WriteLine("</Sprite>");
            }*/
        }

        /// <summary>
        /// Private constructor to prevent instation of a new tile factory
        /// </summary>
        public TileSpriteFactory()
        {
            tileSourceRectangles = FactoryXMLParser.ParseNonAnimatedSpriteXML("DoorAndTileSprites.xml");
            wallSourceRectangles = new Dictionary<int, Rectangle>();
            levelOneSourceRectangles = FactoryXMLParser.ParseNonAnimatedSpriteXML("Level1FloorSprites.xml");
            AddWallSourceRectangles();
        }

        /// <summary>
        /// Load the textures required for the Tile Factory
        /// </summary>
        public void LoadTextures()
        {
            tileSpriteSheet = Texture2DManager.GetTileSheet();
            levelOneSpriteSheet = Texture2DManager.GetLevelOneSpriteSheet();
        }
        /// <summary>
        /// Create and return a new tile sprite
        /// </summary>
        /// <param name="tileName">The specific name of the tile to be created</param>
        /// <returns></returns>
        public ISprite CreateNewTileSprite(string tileName)
        {
            Debug.Assert(tileName != null, "tile is null");
            Debug.Assert(tileSourceRectangles.ContainsKey(tileName), "Source Rectangle does not contain the tile named: " + tileName);
            return new NonAnimatedSprite(tileSourceRectangles[tileName], tileSpriteSheet);

        }

        /// <summary>
        /// Create and return a new wall sprite based on the desired qudrant [1, 2, 3, 4]
        /// </summary>
        /// <param name="quadrant">The quadrant related to where the wall will be placed</param>
        /// <returns></returns>
        public ISprite CreateNewWallSprite(int quadrant)
        {
            Debug.Assert(wallSourceRectangles.ContainsKey(quadrant), "Incorrect Quadrant: " + quadrant);
            return new NonAnimatedSprite(wallSourceRectangles[quadrant], tileSpriteSheet);
        }
        /// <summary>
        /// Create a sprite that will be the room specific floor sprite.
        /// </summary>
        /// <param name="floor">The name of the desired floor sprite</param>
        /// <returns></returns>
        public ISprite CreateFloorSprite(string floor)
        {
            return new NonAnimatedSprite(levelOneSourceRectangles[floor], levelOneSpriteSheet);
        }
    }
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.Factories
{
    public class TileSpriteFactory
    {
        private Texture2D tileSpriteSheet;
        private readonly Dictionary<string, Rectangle> tileSourceRectangles;
        private readonly Dictionary<int, Rectangle> wallSourceRectangles;
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
        public List<string> TileSourceRectangles
        {
            get { return tileSourceRectangles.Keys.ToList(); }
        }

        /// <summary>
        /// Creates a dictionary where the keys are related to the tile names
        /// and each value is related to the source rectangle (spriteOrigin and dimensions)
        /// found on the tile sheet
        /// </summary>
        private void AddTileSourceRectangles()
        {
            string[] tileNames = {
                "flat", "pyramid", "statue1", "statue2",
                "hole", "spackled", "blueflat", "stairs",
                "greybrick", "greystriped"
            };

            int x_pixels = 984, y_pixels = 11; // starting coordiantes of the tiles
            const int WIDTH = 16, HEIGHT = 16; // dimmension of each tile
            foreach (string tile in tileNames)
            {
                // Add tile name with spriteOrigin and dimensions to the dictionary
                tileSourceRectangles.Add(tile, new Rectangle(x_pixels, y_pixels, WIDTH, HEIGHT));
                x_pixels += 17; // move to next column in the current row of tiles
                // when x_pixels exceed 1035, reset the x_pixels and increment y_pixels to access the next row
                if (x_pixels > 1035)
                {
                    x_pixels = 984;
                    y_pixels += 17;
                }
            }
        }

        private void AddWallSourceRectangles()
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

        private void AddDoorSourceRectangles()
        {
            String[] direction = { "north", "west", "east", "south" };
            String[] doorType = { "doorless", "open", "locked", "blocked", "hole" };
            const int ORIGIN_X = 815, ORIGIN_Y = 11;
            Vector2 spriteOrigin = new Vector2(ORIGIN_X, ORIGIN_Y); // top left door tile
            const int WIDTH = 32, HEIGHT = 32, OFFSET = 33;
            foreach (string door_type in doorType)
            {
                foreach (string d in direction)
                {
                    string door = door_type + "_" + d;
                    tileSourceRectangles.Add(door, new Rectangle((int)spriteOrigin.X, (int)spriteOrigin.Y, WIDTH, HEIGHT));
                    spriteOrigin.Y += OFFSET;
                }
                spriteOrigin.X += OFFSET;
                spriteOrigin.Y = ORIGIN_Y;
            }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new tile factory
        /// </summary>
        private TileSpriteFactory()
        {
            tileSourceRectangles = new Dictionary<string, Rectangle>();
            wallSourceRectangles = new Dictionary<int, Rectangle>();
            AddTileSourceRectangles();
            AddWallSourceRectangles();
            AddDoorSourceRectangles();
        }

        public void LoadTextures()
        {
            tileSpriteSheet = Texture2DManager.GetTileSheet();
        }

        public ISprite CreateNewTileSprite(string tileName)
        {
            Debug.Assert(tileName != null, "tile is null");
            Debug.Assert(tileSourceRectangles.ContainsKey(tileName), "Source Rectangle does not contain the tile named: " + tileName);
            return new NonAnimatedSprite(tileSourceRectangles[tileName], tileSpriteSheet);
        }

        public ISprite CreateNewWallSprite(int quadrant)
        {
            Debug.Assert(wallSourceRectangles.ContainsKey(quadrant), "Incorrect Quadrant: " + quadrant);
            Debug.WriteLine(wallSourceRectangles[quadrant]);
            return new NonAnimatedSprite(wallSourceRectangles[quadrant], tileSpriteSheet);
        }
        /* maybe this can be used if we want to separate between doors and tile calls, if not we'll delete it */
        public ISprite CreateNewDoorSprite(string door)
        {
            Debug.Assert(door != null, "door is null");
            Debug.Assert(tileSourceRectangles.ContainsKey(door), "Source Rectangle does not contain the door: " + door);
            Debug.WriteLine(door + " values: " + tileSourceRectangles[door]);
            return new NonAnimatedSprite(tileSourceRectangles[door], tileSpriteSheet);
        }
    }
}
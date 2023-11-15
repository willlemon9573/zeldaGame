﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Size = System.Drawing.Size;

namespace SprintZero1.Factories
{
    public class TileSpriteFactory
    {
        private const string DOOR_TILE_DOCUMENT_PATH = @"XMLFiles\FactoryXMLFiles\ArchitecturalSprites.xml";
        private const string LEVEL_ONE_DOCUMENT_PATH = @"XMLFiles\FactoryXMLFiles\Level1FloorSprites.xml";
        private Texture2D tileSpriteSheet;
        private Texture2D levelOneSpriteSheet;
        private readonly Dictionary<string, Rectangle> _tileSourceRectangles;
        private readonly Dictionary<String, Rectangle> _levelOneSourceRectangles;
        private static readonly TileSpriteFactory instance = new TileSpriteFactory();
        private Texture2D box;

        /// <summary>
        /// Tile Factory is a singleton allowing access to call the Tile Factory whenever needed without creating a new concrete object
        /// </summary>
        public static TileSpriteFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Private constructor to prevent instation of a new tile factory
        /// </summary>
        private TileSpriteFactory()
        {
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            _tileSourceRectangles = spriteParser.ParseNonAnimatedSpriteXML(DOOR_TILE_DOCUMENT_PATH);
            _levelOneSourceRectangles = spriteParser.ParseNonAnimatedSpriteXML(LEVEL_ONE_DOCUMENT_PATH);
            _tileSourceRectangles.Add("box", new Rectangle(3, 3, 16, 16));
        }

        /// <summary>
        /// Load the textures required for the Tile Factory
        /// </summary>
        public void LoadTextures()
        {
            tileSpriteSheet = Texture2DManager.GetTileSheet();
            levelOneSpriteSheet = Texture2DManager.GetLevelOneSpriteSheet();
            box = Texture2DManager.GetBox();
        }
        /// <summary>
        /// Create and return a new tile sprite
        /// </summary>
        /// <param name="tileName">The specific name of the tile to be created</param>
        /// <returns></returns>
        public ISprite CreateNewTileSprite(string tileName)
        {
            Debug.Assert(tileName != null, "tile is null");
            Debug.Assert(_tileSourceRectangles.ContainsKey(tileName), "Source Rectangle does not contain the tile named: " + tileName);
            return new NonAnimatedSprite(_tileSourceRectangles[tileName], tileSpriteSheet);

        }

        /// <summary>
        /// Create a sprite that will be the room specific floor sprite.
        /// </summary>
        /// <param name="floor">The name of the desired floor sprite</param>
        /// <returns></returns>
        public ISprite CreateFloorSprite(string floor)
        {
            return new NonAnimatedSprite(_levelOneSourceRectangles[floor], levelOneSpriteSheet);
        }

        /// <summary>
        /// Get the dimensions of a specific sprite for colliders
        /// </summary>
        /// <param name="tileName">The specific tile that the dimensions are for</param>
        /// <returns>A rectanle that contains the dimensions of the sprite</returns>
        public Size GetSpriteDimensions(string tileName)
        {
            Rectangle tile = _tileSourceRectangles[tileName];
            return new Size(tile.Width, tile.Height);
        }

        public ISprite GetBox()
        {
            return new NonAnimatedSprite(_tileSourceRectangles["box"], box);
        }
    }
}
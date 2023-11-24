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
        private const string DOOR_TILE_DOCUMENT_PATH = @"XMLFiles/FactoryXMLFiles/ArchitecturalSprites.xml";
        private const string LEVEL_ONE_DOCUMENT_PATH = @"XMLFiles/FactoryXMLFiles/Level1FloorSprites.xml";
        private Texture2D tileSpriteSheet;
        private Texture2D levelOneSpriteSheet;
        private readonly Dictionary<string, Rectangle> _tileSourceRectangles;
        private readonly Dictionary<String, Rectangle> _levelOneSourceRectangles;
        private static readonly TileSpriteFactory instance = new TileSpriteFactory();

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
    }
}
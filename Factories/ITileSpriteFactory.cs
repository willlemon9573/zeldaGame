using Microsoft.Xna.Framework.Content;
using SprintZero1.Sprites;
using System.Collections.Generic;


namespace SprintZero1.Factories
{
    internal interface ITileSpriteFactory
    {
        /// <summary>
        /// BlockNameList property to return a list of all the current block names
        /// </summary>
        List<string> BlockNamesList { get; }
        /// <summary>
        /// Loads any and all textures required
        /// </summary>
        void LoadTextures(ContentManager manager);
        /// <summary>
        /// Creates and returns a new OnScreenTile _sprite
        /// </summary>
        /// <param name="blockName">The name of the specific block/tile to be made</param>
        /// 
        /// <returns>a new block of type ISprite</returns>
        ISprite CreateNewTileSprite(string blockName);
    }
}

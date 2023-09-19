using Microsoft.Xna.Framework.Content;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal interface IBlockFactory
    {
        /// <summary>
        /// BlockNameList property to return a list of all the current block names
        /// </summary>
        List<string> BlockNamesList { get; }
        /// <summary>
        /// Initialize the members of this class
        /// </summary>
        void LoadTextures(ContentManager manager);
        /// <summary>
        /// Creates and returns a new NonMovingBlock sprite
        /// </summary>
        /// <param name="blockName">The name of the specific block/tile to be made</param>
        /// <returns>a new block of type ISprite</returns>
        ISprite CreateNonMovingBlockSprite(string blockName);
    }
}

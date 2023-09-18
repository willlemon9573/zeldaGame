using Microsoft.Xna.Framework.Content;
using SprintZero1.Sprites;

namespace SprintZero1.Factories
{
    internal interface IBlockFactory
    {
        /// <summary>
        /// Initialize the members of this class
        /// </summary>
        void Initialize();
        /// <summary>
        /// Loads the texture files for the Block Factory object to cycle through
        /// </summary>
        /// <param name="manager">Content Manager helper</param>
        void LoadTextures(ContentManager manager);

        /// <summary>
        /// Creates and returns a new NonMovingBlock sprite
        /// </summary>
        /// <param name="blockName">The name of the specific block/tile to be made</param>
        /// <returns>a new block of type ISprite</returns>
        ISprite CreateNonMovingBlockSprite(string blockName);
    }
}

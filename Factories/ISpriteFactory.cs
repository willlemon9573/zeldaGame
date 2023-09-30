using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SprintZero1.Factories
{
    internal interface ISpriteFactory
    {
        /// <summary>
        /// SpriteNamesList property to return a list of all the current Sprite names
        /// </summary>
        List<string> SpriteNamesList { get; }


        /// <summary>
        /// Loads any and all textures required
        /// </summary>
        void LoadTextures(ContentManager manager);

        Rectangle Source(string name);
    }
}

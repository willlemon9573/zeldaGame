using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using SprintZero1.Sprites;

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

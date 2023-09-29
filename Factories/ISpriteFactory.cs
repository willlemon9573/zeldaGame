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
        void LoadTextures(ContentManager manager);
        //we are going to pass the direction that the link is going to face for the next frame
        ISprite createNewSprite(Vector2 location, int frameIndex);
    }
}

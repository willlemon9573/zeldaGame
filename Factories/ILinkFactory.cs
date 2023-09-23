using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal interface ILinkFactory
    {
        void LoadTextures(ContentManager manager);
        //we are going to pass the direction that the link is going to face for the next frame
        ISprite createNewLink(int direction, Vector2 position);
    }
}

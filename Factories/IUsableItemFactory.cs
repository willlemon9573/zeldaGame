using Microsoft.Xna.Framework.Content;
using SprintZero1.Sprites;
using System.Collections.Generic;
namespace SprintZero1.Factories
{
    internal interface IUsableItemFactory
    {
        List<string> ItemNamesList { get; }
        void LoadTextures(ContentManager manager);

        ISprite CreateItemSprite(string itemName);
    }
}

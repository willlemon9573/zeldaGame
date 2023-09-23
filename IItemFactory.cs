using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1
{
    internal interface IItemFactory
    {

        List<string> ItemNamesList { get; }
        void LoadTextures(ContentManager manager);
        
        ISprite CreateItemSprite(string itemName);
    }
}

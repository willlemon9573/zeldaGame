using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintSrc.Factories
{
    // For Testing purposes- delete this class when necessary!
    internal class SimpleBlockFactory
    {
        public static readonly SimpleBlockFactory instance = new SimpleBlockFactory();
        private Texture2D BlockSpriteSheet;
        const int WIDTH = 16, HEIGHT = 16;

        Dictionary<int, Rectangle> blockSources = new Dictionary<int, Rectangle>()
        {
            {0, new Rectangle(984, 11, WIDTH, HEIGHT)},
            {1, new Rectangle(1001, 11, WIDTH, HEIGHT)}
        };

        public ISprite ReturnSimpleBlock(int index)
        {
            BlockSpriteSheet = Texture2DManager.GetLevelSpriteSheet();
            return new NonAnimatedSprite(blockSources[index], BlockSpriteSheet);
        }

    }
}

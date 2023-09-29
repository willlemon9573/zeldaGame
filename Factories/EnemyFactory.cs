using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Factories
{
    internal class EnemyFactory : ISpriteFactory
    {
        private Texture2D enemyTexture;
        public ISprite createNewSprite(Vector2 location, int frameIndex)
        {

            throw new NotImplementedException();
        }

        public void LoadTextures(ContentManager manager)
        {
            throw new NotImplementedException();
        }
    }
}

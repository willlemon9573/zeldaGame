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
    public class EnemyFactory : ISpriteFactory
    {
        private Texture2D enemyTexture;
        private static readonly EnemyFactory instance = new EnemyFactory();
        public List<string> SpriteNamesList => throw new NotImplementedException();
        private readonly Dictionary<string, Rectangle> sourceRectangles = new Dictionary<string, Rectangle>();


        public static EnemyFactory Instance
        {
            get { return instance; }
        }

        public void createSpriteDictionary()
        {
            sourceRectangles.Add("enemy1", new Rectangle(230, 122, 16, 16));
            sourceRectangles.Add("enemy2", new Rectangle(230, 105, 16, 16));
            sourceRectangles.Add("enemy3", new Rectangle(262, 227, 24, 32));
            sourceRectangles.Add("enemy4", new Rectangle(40, 154, 32, 32));
        }

        public void LoadTextures(ContentManager manager)
        {
            throw new NotImplementedException();
        }

        public Rectangle Source(string name)
        {
            return sourceRectangles[name];
        }
    }
}

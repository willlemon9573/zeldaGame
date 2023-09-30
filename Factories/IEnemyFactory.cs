using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.Sprites;

namespace SprintZero1.Factories
{
    internal interface IEnemyFactory
    {
        

        List<string> EnemyNamesList { get; }

        
        void LoadTextures(ContentManager manager);

        ISprite CreateEnemySprite(string enemyName, Vector2 locationm, int FrameIndex);
        


    }
}

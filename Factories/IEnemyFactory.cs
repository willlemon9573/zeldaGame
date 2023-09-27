using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Factories
{
    internal interface IEnemyFactory
    {
        

        List<string> EnemyNamesList { get; }

        
        void LoadTextures(ContentManager manager);

        ISprite CreateEnemySprite(string enemyName, Vector2 location);
        


    }
}

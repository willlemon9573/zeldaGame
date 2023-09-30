using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SprintZero1.Factories
{
    
    public class EnemyFactory : IEnemyFactory
    {
        private Texture2D dungeonEnemySpritesheet, overworldEnemySpritesheet, bossSpritesheet;
        private readonly List<string> enemyNamesList;
        private readonly Dictionary<string, List<Rectangle>> enemyType;
        const int HEIGHT = 16, WIDTH = 16;
        

        public static EnemyFactory Instance
        {
            get { return Instance; }
        }

        private void CreateEnemyDictionary()
        {
            //DUNGEON ENEMY SPRITESHEET
            //gel
            enemyType["gel"] = new List<Rectangle>
            {
                new Rectangle(1, 11, 8, HEIGHT),
                new Rectangle(10, 11, 8, HEIGHT)
            };

            //zol
            enemyType["zol"] = new List<Rectangle>
            {
                new Rectangle(77, 11, WIDTH, HEIGHT),
                new Rectangle(94, 11, WIDTH, HEIGHT)
            };
            //keese
            enemyType["keese"] = new List<Rectangle>
            {
                new Rectangle(183, 11, WIDTH, HEIGHT),
                new Rectangle(200, 11, WIDTH, HEIGHT)
            };
            //goriya
            enemyType["goriya"] = new List<Rectangle>
            {
                new Rectangle(222, 11, WIDTH, HEIGHT),
                new Rectangle(239, 11, WIDTH, HEIGHT),
                new Rectangle(256, 11, WIDTH, HEIGHT),
                new Rectangle(273, 11, WIDTH, HEIGHT)
            };
            //bubble
            enemyType["bubble"] = new List<Rectangle>
            {
                new Rectangle(321, 11, WIDTH, HEIGHT)
            };
            //wallmaster
            enemyType["wallmaster"] = new List<Rectangle>
            {
                new Rectangle(393, 11, WIDTH, HEIGHT),
                new Rectangle(410, 11, WIDTH, HEIGHT)
            };
            //OVERWORLD ENEMY SPRITESHEET
            //
            enemyType["octorok_up"] = new List<Rectangle>
            {
                new Rectangle(1, 11, WIDTH, HEIGHT),
                new Rectangle(18, 11, WIDTH, HEIGHT)
            };

            enemyType["octorok_side"] = new List<Rectangle>
            {
                new Rectangle(35, 11, WIDTH, HEIGHT),
                new Rectangle(52, 11, WIDTH, HEIGHT)
            };

            enemyType["moblin_up"] = new List<Rectangle>
            {
                new Rectangle(82, 11, WIDTH, HEIGHT),
                new Rectangle(99, 11, WIDTH, HEIGHT)
            };

            enemyType["moblin_side"] = new List<Rectangle>
            {
                new Rectangle(116, 11, WIDTH, HEIGHT),
                new Rectangle(133, 11, WIDTH, HEIGHT)
            };

            enemyType["leever"] = new List<Rectangle>
            {
                new Rectangle(1, 59, WIDTH, HEIGHT),
                new Rectangle(18, 59, WIDTH, HEIGHT),
                new Rectangle(35, 59, WIDTH, HEIGHT),
                new Rectangle(52, 59, WIDTH, HEIGHT),
                new Rectangle(69, 59, WIDTH, HEIGHT)
            };

            enemyType["peahat"] = new List<Rectangle>
            {
                new Rectangle(162, 59, WIDTH, HEIGHT),
                new Rectangle(179, 59, WIDTH, HEIGHT)
            };

            enemyType["armos_up"] = new List<Rectangle>
            {
                new Rectangle(234, 90, WIDTH, HEIGHT),
                new Rectangle(251, 90, WIDTH, HEIGHT)
            };

            enemyType["armos_down"] = new List<Rectangle>
            {
                new Rectangle(268, 90, WIDTH, HEIGHT),
                new Rectangle(285, 90, WIDTH, HEIGHT)
            };

            enemyType["tektite"] = new List<Rectangle>
            {
                new Rectangle(162, 90, WIDTH, HEIGHT),
                new Rectangle(179, 90, WIDTH, HEIGHT)
            };

        }

        private void CreateBossDictionary()
        {
            enemyType["aquamentus"] = new List<Rectangle>
            {
                new Rectangle(1, 11, 24, 32),
                new Rectangle(26, 11, 24, 32),
                new Rectangle(51, 11, 24, 32),
                new Rectangle(76, 11, 24, 32)
            };

            enemyType["digdogger"] = new List<Rectangle>
            {
                new Rectangle(196, 58, 32, 32),
                new Rectangle(229, 58, 32, 32),
                new Rectangle(262, 58, 32, 32),
                new Rectangle(295, 58, 32, 32),
                new Rectangle(328, 58, 32, 32)
            };

            enemyType["ganon"] = new List<Rectangle>
            {
                new Rectangle(40, 154, 32, 32),
                new Rectangle(73, 154, 32, 32),
                new Rectangle(106, 154, 32, 32),
                new Rectangle(139, 154, 32, 32),
                new Rectangle(172, 154, 32, 32),
                new Rectangle(205, 154, 32, 32)
            };
        }

        private EnemyFactory()
        {
            enemyNamesList = new List<string>()
            {
                "gel", "zol", "keese", "goriya", "bubble", "wallmaster",
                "octorok_up", "octorok_side", "moblin_up", "moblin_side",
                "leever", "peahat", "armos_up", "armos_down", "tektite",
                "aquamentus", "digdogger", "ganon"
            };
            enemyType = new Dictionary<string, List<Rectangle>>();
            CreateEnemyDictionary();
            CreateBossDictionary();
        }

        public List<string> EnemyNamesList
        {
            get { return EnemyNamesList; }
        }

        public void LoadTextures(ContentManager manager)
        {
            dungeonEnemySpritesheet = manager.Load<Texture2D>("DungeonEnemySpritesheet");
            overworldEnemySpritesheet = manager.Load<Texture2D>("OverworldEnemySpritesheet");
            bossSpritesheet = manager.Load<Texture2D>("BossSpritesheet");
        }

        public ISprite CreateEnemySprite(string enemyName, Microsoft.Xna.Framework.Vector2 location)
        {
            Debug.Assert(enemyName != null, "enemyName is null");
            Debug.Assert();
            return;
        }

        
    }
}

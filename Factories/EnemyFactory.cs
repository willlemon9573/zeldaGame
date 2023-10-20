using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.Factories
{

    public class EnemyFactory
    {
        private Texture2D dungeonEnemySpriteSheet, bossSpriteSheet;
        private readonly Dictionary<string, List<Rectangle>> enemySpriteDictionary;
        private readonly Dictionary<string, List<Rectangle>> bossEnemySpriteDictionary;
        private static readonly EnemyFactory instance = new EnemyFactory();
        const int HEIGHT = 16, WIDTH = 16;

        public static EnemyFactory Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Get the regular enemy names as a list 
        /// </summary>
        public List<string> EnemyNamesList
        {
            get { return enemySpriteDictionary.Keys.ToList<string>(); }
        }
        /// <summary>
        /// Get the boss enemy names as a list 
        /// </summary>
        public List<string> BossNameList
        {
            get { return bossEnemySpriteDictionary.Keys.ToList<string>(); }
        }
        /// <summary>
        /// Creates the rectangles required for each frame of each enemy and places in the proper dictionary
        /// </summary>
        private void CreateEnemySpriteDictionary()
        {
            //DUNGEON ENEMY SPRITESHEET
            //gel
            enemySpriteDictionary["dungeon_gel"] = new List<Rectangle>
            {
                new Rectangle(1, 11, 8, HEIGHT),
                new Rectangle(10, 11, 8, HEIGHT)
            };

            //zol
            enemySpriteDictionary["dungeon_zol"] = new List<Rectangle>
            {
                new Rectangle(77, 11, WIDTH, HEIGHT),
                new Rectangle(94, 11, WIDTH, HEIGHT)
            };
            //keese

            enemySpriteDictionary["dungeon_keese"] = new List<Rectangle>
            {
                new Rectangle(183, 11, WIDTH, HEIGHT),
                new Rectangle(200, 11, WIDTH, HEIGHT)
            };

            //wallmaster
            enemySpriteDictionary["dungeon_wallmaster"] = new List<Rectangle>
            {
                new Rectangle(393, 11, WIDTH, HEIGHT),
                new Rectangle(410, 11, WIDTH, HEIGHT)
            };
        }
        /// <summary>
        /// Creates the rectangles required for each frame of each boss and places in the proper dictionary
        /// </summary>
        private void CreateBossDictionary()
        {
            bossEnemySpriteDictionary["aquamentus"] = new List<Rectangle>
            {
                new Rectangle(1, 11, 24, 32),
                new Rectangle(26, 11, 24, 32),
                new Rectangle(51, 11, 24, 32),
                new Rectangle(76, 11, 24, 32)
            };

            bossEnemySpriteDictionary["digdogger"] = new List<Rectangle>
            {
                new Rectangle(196, 58, 32, 32),
                new Rectangle(229, 58, 32, 32),
                new Rectangle(262, 58, 32, 32),
                new Rectangle(295, 58, 32, 32),
                new Rectangle(328, 58, 32, 32)
            };

            bossEnemySpriteDictionary["ganon"] = new List<Rectangle>
            {
                new Rectangle(40, 154, 32, 32),
                new Rectangle(73, 154, 32, 32),
                new Rectangle(106, 154, 32, 32),
                new Rectangle(139, 154, 32, 32),
                new Rectangle(172, 154, 32, 32),
                new Rectangle(205, 154, 32, 32)
            };
        }

        /// <summary>
        /// Private constructor to prevent instantiation of the singleton
        /// </summary>
        private EnemyFactory()
        {
            enemySpriteDictionary = new Dictionary<string, List<Rectangle>>();
            bossEnemySpriteDictionary = new Dictionary<string, List<Rectangle>>();
            CreateEnemySpriteDictionary();
            CreateBossDictionary();
        }
        /// <summary>
        /// Load the textures required for the enemy sprites
        /// </summary>
        public void LoadTextures()
        {
            dungeonEnemySpriteSheet = Texture2DManager.GetEnemySpriteSheet();
            bossSpriteSheet = Texture2DManager.GetBossSpriteSheet();
        }
        /// <summary>
        /// Creates the enemy sprite
        /// </summary>
        /// <param name="enemyName">The name of the enemy/param>
        /// <param name="totalFrames">The maximum amount of frames for the sprite</param>
        /// <returns>An animated sprite of the enemy</returns>
        public ISprite CreateEnemySprite(string enemyName, int totalFrames)
        {
            Debug.Assert(enemyName != null, "enemyName is null");

            Debug.Assert(enemySpriteDictionary.ContainsKey(enemyName), "Enemy not found: " + enemyName);
            return new AnimatedSprite(enemySpriteDictionary[enemyName], dungeonEnemySpriteSheet, totalFrames);
        }
        /// <summary>
        /// Creates a boss sprite
        /// </summary>
        /// <param name="bossName">The name of the boss/param>
        /// <param name="totalFrames">The maximum amount of frames for the sprite</param>
        /// <returns>An animated sprite of the boss</returns>
        public ISprite CreateBossSprite(string bossName, int totalFrames)
        {
            Debug.Assert(bossEnemySpriteDictionary.ContainsKey(bossName), "Boss not found: " + bossName);
            return new AnimatedSprite(bossEnemySpriteDictionary[bossName], bossSpriteSheet, totalFrames);
        }
    }
}

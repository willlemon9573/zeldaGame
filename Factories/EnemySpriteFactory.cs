using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{

    public class EnemySpriteFactory
    {
        private Texture2D dungeonEnemySpriteSheet, bossSpriteSheet;
        private readonly Dictionary<string, List<Rectangle>> enemySpriteDictionary;
        private readonly Dictionary<string, List<Rectangle>> bossEnemySpriteDictionary;
        private static readonly EnemySpriteFactory instance = new EnemySpriteFactory();
        /// <summary>
        /// Get the Enemy Sprite Factory instance
        /// </summary>
        public static EnemySpriteFactory Instance
        {
            get { return instance; }
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
        /// Private constructor to prevent instantiation of the singleton
        /// </summary>
        private EnemySpriteFactory()
        {
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            enemySpriteDictionary = spriteParser.ParseAnimatedSpriteXML(@"XMLFiles\FactoryXMLFiles\EnemySprites.xml");
            bossEnemySpriteDictionary = spriteParser.ParseAnimatedSpriteXML(@"XMLFiles\FactoryXMLFiles\BossSprites.xml");
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
            Debug.Assert(totalFrames >= 0, "totalFrames must be positive");
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
            Debug.Assert(bossName != null, "bossName is null");
            Debug.Assert(totalFrames >= 0, "totalFrames must be positive");
            Debug.Assert(bossEnemySpriteDictionary.ContainsKey(bossName), "Boss not found: " + bossName);
            return new AnimatedSprite(bossEnemySpriteDictionary[bossName], bossSpriteSheet, totalFrames);
        }
    }
}

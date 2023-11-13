using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{

    public class EnemySpriteFactory
    {
        private const string ENEMY_SPRITE_PATH = @"XMLFiles\FactoryXMLFiles\EnemySprites.xml";
        private const string BOSS_SPRITE_PATH = @"XMLFiles\FactoryXMLFiles\BossSprites.xml";
        private Texture2D dungeonEnemySpriteSheet, bossSpriteSheet;
        private readonly Dictionary<string, List<Rectangle>> enemySpriteWithoutDirectionDictionary;
        private readonly Dictionary<string, Dictionary<Direction, List<Rectangle>>> enemySpriteWithDirectionDictionary;
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
            enemySpriteWithoutDirectionDictionary = spriteParser.ParseAnimatedSpriteXML(ENEMY_SPRITE_PATH);
            bossEnemySpriteDictionary = spriteParser.ParseAnimatedSpriteXML(BOSS_SPRITE_PATH);
            enemySpriteWithDirectionDictionary = new Dictionary<string, Dictionary<Direction, List<Rectangle>>>
            {
                {
                    "dungeon_goriya", new Dictionary<Direction, List<Rectangle>>
                    {
                        { Direction.North, new List<Rectangle> { new Rectangle(241, 11, 13, 16), new Rectangle(308, 11, 13, 16) } },
                        { Direction.South, new List<Rectangle> { new Rectangle(224, 11, 13, 16), new Rectangle(292, 11, 13, 16) } },
                        { Direction.West, new List<Rectangle> { new Rectangle(257, 11, 13, 16), new Rectangle(275, 11, 14, 16) } },
                        { Direction.East, new List<Rectangle> { new Rectangle(257, 11, 13, 16), new Rectangle(275, 11, 14, 16) } }
                    }
                },
                {
                    "dungeon_wallmaster", new Dictionary<Direction, List<Rectangle>>
                    {
                        { Direction.North, new List<Rectangle> { new Rectangle(241, 11, 13, 16), new Rectangle(308, 11, 13, 16) } },
                        { Direction.South, new List<Rectangle> { new Rectangle(224, 11, 13, 16), new Rectangle(292, 11, 13, 16) } },
                        { Direction.West, new List<Rectangle> { new Rectangle(257, 11, 13, 16), new Rectangle(275, 11, 14, 16) } },
                        { Direction.East, new List<Rectangle> { new Rectangle(257, 11, 13, 16), new Rectangle(275, 11, 14, 16) } }
                    }
                }
            };
        }


        /// <summary>
        /// Creates the enemy sprite
        /// </summary>
        /// <param name="enemyName">The name of the enemy/param>
        /// <returns>An animated sprite of the enemy</returns>
        public ISprite CreateEnemySprite(string enemyName, Direction direction)
        {
            Debug.Assert(enemyName != null, "enemyName is null");
            //Debug.Assert(totalFrames >= 0, "totalFrames must be positive");
            //Debug.Assert(enemySpriteWithoutDirectionDictionary.ContainsKey(enemyName), "Enemy not found: " + enemyName);
            if (enemyName == "dungeon_goriya" || enemyName == "dungeon_wallmaster")
            {
                Dictionary<Direction, List<Rectangle>> DirectionRec = enemySpriteWithDirectionDictionary[enemyName];
                return new AnimatedSprite(DirectionRec[direction], dungeonEnemySpriteSheet, enemySpriteWithoutDirectionDictionary[enemyName].Count);
            }
            else
            {
                return new AnimatedSprite(enemySpriteWithoutDirectionDictionary[enemyName], dungeonEnemySpriteSheet, enemySpriteWithoutDirectionDictionary[enemyName].Count);
            }
        }

        /// <summary>
        /// Creates a boss sprite
        /// </summary>
        /// <param name="bossName">The name of the boss/param>
        /// <param name="totalFrames">The maximum amount of frames for the sprite</param>
        /// <returns>An animated sprite of the boss</returns>
        public ISprite CreateBossSprite(string bossName)
        {
            Debug.Assert(bossName != null, "bossName is null");
            //Debug.Assert(bossEnemySpriteDictionary.ContainsKey(bossName), "Boss not found: " + bossName);
            return new AnimatedSprite(bossEnemySpriteDictionary[bossName], bossSpriteSheet, bossEnemySpriteDictionary[bossName].Count);
        }
    }
}

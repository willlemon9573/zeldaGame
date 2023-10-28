using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    internal class WeaponSpriteFactory
    {
        /* Temporary class for sprint2 requirements. This along with other factories will be refractored for sprint 3 */
        private Texture2D spriteSheet;
        private Texture2D enemyProjectileSheet;
        private readonly Dictionary<string, List<Rectangle>> projectileSourceRectangles;
        private static readonly WeaponSpriteFactory instance = new WeaponSpriteFactory();
        private readonly Dictionary<Direction, Rectangle> _linkSwordSpriteDictionary;
        /// <summary>
        /// Get the weapon sprite factory instance
        /// </summary>
        public static WeaponSpriteFactory Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Load the sprite sheets related to weapon/projectile
        /// </summary>
        public void LoadTextures()
        {
            spriteSheet = Texture2DManager.GetLinkSpriteSheet();
            enemyProjectileSheet = Texture2DManager.GetBossSpriteSheet();
        }
        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private WeaponSpriteFactory()
        {
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            projectileSourceRectangles = spriteParser.ParseAnimatedSpriteXML(@"XMLFiles\FactoryXMLFiles\ProjectileSprites.xml");
            _linkSwordSpriteDictionary = spriteParser.ParseNonAnimatedSpriteWithDirectionXML(@"XMLFiles\FactoryXMLFiles\LinkSwordSprites.xml");
        }

        public ISprite CreateAquamentusWeaponSprite(int index)
        {
            List<Rectangle> sourceRectangle = projectileSourceRectangles["aquamentusWeapon"];
            return new NonAnimatedSprite(sourceRectangle[index], enemyProjectileSheet);
        }

        public ISprite CreateBoomerangSprite(String weaponType)
        {
            List<Rectangle> sourceRectangle = projectileSourceRectangles["boomerang"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle = projectileSourceRectangles["betterboomerang"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return new NonAnimatedSprite(sourceRectangle[0], spriteSheet);
        }
        public ISprite CreateEndSprite()
        {
            return new NonAnimatedSprite(new Rectangle(53, 190, 8, 8), spriteSheet);
        }

        public ISprite CreateArrowSprite(String weaponType, Direction direction)
        {
            List<Rectangle> sourceRectangle = projectileSourceRectangles["arrow"];
            int index = 0;
            if (direction == Direction.East || direction == Direction.West)
            {
                index = 1;
            }
            if (weaponType.Equals("better"))
            {
                sourceRectangle = projectileSourceRectangles["betterbowarrows"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return new NonAnimatedSprite(sourceRectangle[index], spriteSheet);
        }

        public ISprite CreateBombSprite()
        {
            List<Rectangle> sourceRectangle = projectileSourceRectangles["bomb"];
            return new NonAnimatedSprite(sourceRectangle[0], spriteSheet);
        }

        public ISprite CreateBombSpriteExplodes()
        {
            int maxFrame = 3;
            List<Rectangle> sourceRectangle = projectileSourceRectangles["bomb"];
            List<Rectangle> newRectangleList = sourceRectangle.GetRange(1, 3);
            return new ControlledAnimation(new AnimatedSprite(newRectangleList, spriteSheet, maxFrame), maxFrame);
        }
        public ISprite CreateMagicFireSprite()
        {
            int maxFrame = 2;
            List<Rectangle> sourceRectangle = projectileSourceRectangles["magicfire"];
            return new AnimatedSprite(sourceRectangle, spriteSheet, maxFrame);
        }

        /// <summary>
        /// Get player's weapon sprite relative to his direction when he attacks
        /// </summary>
        /// <param name="direction">the direction the player is facing</param>
        /// <returns></returns>
        public ISprite GetSwordSprite(Direction direction)
        {
            Debug.Assert(_linkSwordSpriteDictionary.ContainsKey(direction), "Direction not found: " + direction);
            return new NonAnimatedSprite(_linkSwordSpriteDictionary[direction], spriteSheet);
        }
    }
}

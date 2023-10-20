using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Managers;
using SprintZero1.Enums;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.Factories
{
    internal class WeaponSpriteFactory
    {
        /* Temporary class for sprint2 requirements. This along with other factories will be refractored for sprint 3 */
        private Texture2D spriteSheet;
        private readonly Dictionary<string, List<Rectangle>> weaponSourceRectangles;
        private static readonly WeaponSpriteFactory instance = new WeaponSpriteFactory();

        private readonly Dictionary<(String, Direction), Rectangle> _meleeWeaponSourceRectangle;

        public static WeaponSpriteFactory Instance
        {
            get { return instance; }
        }
        private void CreateDictionary()
        {
            /* starting position of each weapon */
            /*weaponSourceRectangles.Add("arrow", new Rectangle(2, 190, 16, 5));
            weaponSourceRectangles.Add("boomerang", new Rectangle(57, 189, 8, 8));
            weaponSourceRectangles.Add("bomb", new Rectangle(130, 185, 70, 16));
            weaponSourceRectangles.Add("magicfire", new Rectangle(202, 185, 70, 16));
            */

            List<Rectangle> regBoomerangFrames = new List<Rectangle>
            {
                new Rectangle(63, 189, 8, 8)
            };

            List<Rectangle> betterBoomerangFrames = new List<Rectangle>
            {
                new Rectangle(91, 189, 8, 8)
            };

            List<Rectangle> regArrowFrames = new List<Rectangle>
            {
                new Rectangle(3, 185, 5, 16),
                new Rectangle(10, 190, 16, 5)
            };
            List<Rectangle> betterArrowFrames = new List<Rectangle> {
                new Rectangle(29, 182, 5, 16),
                new Rectangle(36, 190, 16, 5)
            };

            List<Rectangle> bombFrames = new List<Rectangle>();
            List<Rectangle> fireFrames = new List<Rectangle> {
                new Rectangle(194,185,16,16),
                new Rectangle(213,185,16,16)
            };
            int bombX = 126, y = 185, width = 16, height = 16;
            for (int i = 0; i < 4; i++)
            {
                bombFrames.Add(new Rectangle(bombX, y, width, height));
                if (i >= 1)
                {
                    bombX += width + 2;
                }
                else
                {
                    bombX += width;
                }
            }
            weaponSourceRectangles.Add("boomerang", regBoomerangFrames);
            weaponSourceRectangles.Add("betterboomerang", betterBoomerangFrames);
            weaponSourceRectangles.Add("arrow", regArrowFrames);
            weaponSourceRectangles.Add("betterbowarrows", betterArrowFrames);
            weaponSourceRectangles.Add("bomb", bombFrames);
            weaponSourceRectangles.Add("magicfire", fireFrames);
        }


        private void CreateMeleeWeaponDictionary()
        {
            List<Direction> directions = new List<Direction>() { Direction.North, Direction.South, Direction.East, Direction.West };
            // woodensword
            List<Rectangle> coordsList = new List<Rectangle>
            {
                new Rectangle( 1, 154, 7, 16),
                new Rectangle(10, 159, 16, 7)
            };
            for (int i = 0; i < directions.Count; i++)
            {
                Rectangle r;
                if (i < 2)
                {
                    r = coordsList[0];
                }
                else
                {
                    r = coordsList[1];
                }
                _meleeWeaponSourceRectangle.Add(("woodensword", directions[i]), r);
            }

        }
        public void LoadTextures()
        {
            spriteSheet = Texture2DManager.GetLinkSpriteSheet();
        }
        private WeaponSpriteFactory()
        {
            weaponSourceRectangles = new Dictionary<string, List<Rectangle>>();
            _meleeWeaponSourceRectangle = new Dictionary<(string, Direction), Rectangle>();
            CreateDictionary();

            CreateMeleeWeaponDictionary();
        }
        

        public ISprite CreateBoomerangSprite(String weaponType,  Direction direction)
        {
            List<Rectangle> sourceRectangle = weaponSourceRectangles["boomerang"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle = weaponSourceRectangles["betterboomerang"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return new NonAnimatedSprite(sourceRectangle[0], spriteSheet); ;
        }
        public ISprite CreateEndSprite()
        {
            return new NonAnimatedSprite(new Rectangle(53, 190, 8, 8), spriteSheet);
        }
        public ISprite CreateArrowSprite(String weaponType,  Direction direction)
        {
            List<Rectangle> sourceRectangle = weaponSourceRectangles["arrow"];
            int index = 0;
            if(direction == Direction.East || direction == Direction.West)
            {
                index = 1;
            }
            if (weaponType.Equals("better"))
            {
                sourceRectangle = weaponSourceRectangles["betterbowarrows"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return new NonAnimatedSprite(sourceRectangle[index],spriteSheet);
        }

        public ISprite CreateBombSprite()
        {
            List<Rectangle> sourceRectangle = weaponSourceRectangles["bomb"];
            /*   return new WeaponSprite(location, weaponSourceRectangles["bomb"], this.spriteSheet, maxFrames, direction);*/
            return new NonAnimatedSprite(sourceRectangle[0], spriteSheet);
        }

        public ISprite CreateBombSpriteExplodes()
        {
            int maxFrame = 3;
            List<Rectangle> sourceRectangle = weaponSourceRectangles["bomb"];
            List<Rectangle> newRectangleList = sourceRectangle.GetRange(1, 3);
            /*   return new WeaponSprite(location, weaponSourceRectangles["bomb"], this.spriteSheet, maxFrames, direction);*/
            return new ControlledAnimation(new AnimatedSprite(newRectangleList, spriteSheet, maxFrame),maxFrame);
        }
        public ISprite CreateMagicFireSprite()
        {
            int maxFrame = 2;
            List<Rectangle> sourceRectangle = weaponSourceRectangles["magicfire"];
            /* return new WeaponSprite(location, weaponSourceRectangles["magicfire"], this.spriteSheet, maxFrames, direction);*/
            return new AnimatedSprite(sourceRectangle,spriteSheet, maxFrame);
        }

        public ISprite GetMeleeWeaponSprite(String weaponName, Direction direction)
        {
            Debug.Assert(_meleeWeaponSourceRectangle.ContainsKey((weaponName, direction)), "Meelee weapon not found: " + weaponName);
            return new NonAnimatedSprite(_meleeWeaponSourceRectangle[(weaponName, direction)], spriteSheet);
        }
    }
}

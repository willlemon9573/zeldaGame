﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
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
                new Rectangle(57, 187, 7, 12),
                new Rectangle(64, 187, 10, 12),
                new Rectangle(74, 185, 10, 12),
                new Rectangle(84, 187, 10, 12),
                new Rectangle(94, 187, 7, 12),
                new Rectangle(101, 187, 10, 12)
            };

            List<Rectangle> betterBoomerangFrames = new List<Rectangle>
            {
                new Rectangle(122, 187, 7, 11),
                new Rectangle(129, 187, 10, 11),
                new Rectangle(139, 187, 10, 11),
                new Rectangle(149, 187, 10, 11),
                new Rectangle(159, 187, 7, 11),
                new Rectangle(166, 187, 10, 11),
                new Rectangle(176, 187, 10, 11),
                new Rectangle(186, 187, 10, 11)
            };

            List<Rectangle> regArrowFrames = new List<Rectangle>
            {
                new Rectangle(0, 185, 19, 20)
            };
            List<Rectangle> betterArrowFrames = new List<Rectangle> {
                new Rectangle(28, 185, 19, 15)
            };

            List<Rectangle> bombFrames = new List<Rectangle>();
            List<Rectangle> fireFrames = new List<Rectangle>();
            int bombX = 208, fireX = 278, y = 185, width = 16, height = 16;
            for (int i = 0; i < 4; i++)
            {
                bombFrames.Add(new Rectangle(bombX, y, width, height));
                fireFrames.Add(new Rectangle(fireX, y, width, height));
                if (i >= 1)
                {
                    bombX += width + 2;
                }
                else
                {
                    bombX += width;
                }
                fireX += width + 2;
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
                /* for testing - to be removed in the future */
                _meleeWeaponSourceRectangle.Add(("woodensword", directions[i]), r);
            }
        }

        private WeaponSpriteFactory()
        {
            weaponSourceRectangles = new Dictionary<string, List<Rectangle>>();
            _meleeWeaponSourceRectangle = new Dictionary<(string, Direction), Rectangle>();
            CreateDictionary();
            CreateMeleeWeaponDictionary();
        }

        public void LoadTextures()
        {
            spriteSheet = Texture2DManager.GetWeaponSpriteSheet();
        }

        public ISprite CreateBoomerangSprite(String weaponType, Vector2 location, int maxFrames, int direction)
        {
            List<Rectangle> sourceRectangle = weaponSourceRectangles["boomerang"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle = weaponSourceRectangles["betterboomerang"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return null;
        }

        public ISprite CreateArrowSprite(String weaponType, Vector2 location, int maxFrames, int direction)
        {
            List<Rectangle> sourceRectangle = weaponSourceRectangles["arrow"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle = weaponSourceRectangles["betterbowarrows"];
            }
            /*  return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);*/
            return null;
        }

        public ISprite CreateBombSprite(Vector2 location, int maxFrames, int direction)
        {
            /*   return new WeaponSprite(location, weaponSourceRectangles["bomb"], this.spriteSheet, maxFrames, direction);*/
            return null;
        }

        public ISprite CreateMagicFireSprite(Vector2 location, int maxFrames, int direction)
        {
            /* return new WeaponSprite(location, weaponSourceRectangles["magicfire"], this.spriteSheet, maxFrames, direction);*/
            return null;
        }

        public ISprite GetMeleeWeaponSprite(String weaponName, Direction direction)
        {
            Debug.Assert(_meleeWeaponSourceRectangle.ContainsKey((weaponName, direction)), "Meelee weapon not found: " + weaponName);
            return new NonAnimatedSprite(_meleeWeaponSourceRectangle[(weaponName, direction)], spriteSheet);
        }
    }
}

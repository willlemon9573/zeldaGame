using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class WeaponSpriteFactory
    {
        /* Temporary class for sprint2 requirements. This along with other factories will be refractored for sprint 3 */
        private Texture2D spriteSheet;
        private readonly Dictionary<string, Rectangle> weaponSourceRectangles;
        private static readonly WeaponSpriteFactory instance = new WeaponSpriteFactory();


        public static WeaponSpriteFactory Instance
        {
            get { return instance; }
        }
        private void CreateDictionary()
        {
            /* starting position of each weapon */
            weaponSourceRectangles.Add("arrow", new Rectangle(2, 190, 16, 5));
            weaponSourceRectangles.Add("boomerang", new Rectangle(57, 189, 8, 8));
            weaponSourceRectangles.Add("bomb", new Rectangle(130, 185, 70, 16));
            weaponSourceRectangles.Add("magicfire", new Rectangle(202, 185, 70, 16));
        }

        private WeaponSpriteFactory() {
            weaponSourceRectangles = new Dictionary<string, Rectangle>();
            CreateDictionary();
        }

        public void LoadTextures(ContentManager manager)
        {
            spriteSheet = manager.Load<Texture2D>("LinkSheet");
        }

        public ISprite CreateBoomerangSprite(String weaponType, Vector2 location, int maxFrames, int direction)
        {
            Rectangle sourceRectangle = weaponSourceRectangles["boomerang"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle.X = 94;
            }
            return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);
        }

        public ISprite CreateArrowSprite(String weaponType, Vector2 location, int maxFrames, int direction)
        {
            Rectangle sourceRectangle = weaponSourceRectangles["arrow"];
            if (weaponType.Equals("better"))
            {
                sourceRectangle.X = 30;
            }
            return new WeaponSprite(location, sourceRectangle, this.spriteSheet, maxFrames, direction);
        }

        public ISprite CreateBombSprite(Vector2 location, int maxFrames, int direction)
        {
            return new WeaponSprite(location, weaponSourceRectangles["bomb"], this.spriteSheet, maxFrames, direction);
        }

        public ISprite CreateMagicFireSprite(Vector2 location, int maxFrames, int direction)
        {
            return new WeaponSprite(location, weaponSourceRectangles["magicfire"], this.spriteSheet, maxFrames, direction);
        }
    }
}

using SprintZero1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintZero1.XMLParsers;
using SprintZero1.Sprites;

namespace SprintZero1.Factories
{
    public class HUDSpriteFactory
    {

        private const string HUD_SPRITE_PATH = @"XMLFiles\HUDXMLFiles\HUDSprites.xml";
        private const string ANIMATED_HUD_PATH=@"XMLFiles\HUDXMLFiles\HUDAnimatedSprites.xml";
        public Texture2D hudSpriteSheet;
        private Dictionary<string, ISprite> HUDSpriteDictionary;
        private  Dictionary<string, List<Rectangle>> animatedHUDSpriteMap;
        private static readonly HUDSpriteFactory instance = new HUDSpriteFactory();

        public static HUDSpriteFactory Instance
        {
            get { return instance; }
        }

        public void LoadTextures()
        {
            hudSpriteSheet = Texture2DManager.GetHUDSpriteSheet();
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            HUDSpriteDictionary = spriteParser.ParseHUDSprites(HUD_SPRITE_PATH, hudSpriteSheet);
            //animatedHUDSpriteMap = spriteParser.ParseAnimatedSpriteXML(ANIMATED_HUD_PATH);
        }


        public ISprite CreateHUDSprite(string name)
        {
            return HUDSpriteDictionary[name];
        }
        public ISprite CreateAnimatedItemSprite(string name)
        {
            return new AnimatedSprite(animatedHUDSpriteMap[name], hudSpriteSheet,2);
        }
    }
}

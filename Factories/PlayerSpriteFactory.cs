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
    public class PlayerSpriteFactory
    {
        private const string MovementXMLPath = @"XMLFiles/FactoryXMLFiles/LinkMovingSprites.xml";
        private const string AttackingXMLPath = @"XMLFiles/FactoryXMLFiles/LinkAttackingSprites.xml";
        private const int AnimatedSpriteFrames = 2;
        private const string Link = "Link";
        private const string Zelda = "Zelda";
        private readonly Dictionary<string, Texture2D> _playerTextureMap;
        private Texture2D LinkSpriteSheet;
        private readonly Dictionary<Direction, List<Rectangle>> movementSpriteDictionary;
        private readonly Dictionary<Direction, Rectangle> attackSpriteDictionary;
        private readonly Dictionary<(string, Direction), List<Rectangle>> _playerMovementMap;
        private readonly Dictionary<(string, Direction), Rectangle> _playerAttackingMap;
        private static readonly PlayerSpriteFactory instance = new();
        /// <summary>
        /// Get the instance of the factory
        /// </summary>
        public static PlayerSpriteFactory Instance
        {
            get { return instance; }
        }

       

        /// <summary>
        /// Private constructor to prevent instation of a new block factory
        /// </summary>
        private PlayerSpriteFactory()
        {
            SpriteXMLParser spriteParser = new SpriteXMLParser();
            movementSpriteDictionary = spriteParser.ParseAnimatedSpriteWithDirectionXML(MovementXMLPath);
            attackSpriteDictionary = spriteParser.ParseNonAnimatedSpriteWithDirectionXML(AttackingXMLPath);
            _playerMovementMap = spriteParser.ParseAnimatedSpriteWithDirectionXML(MovementXMLPath,Link);
            _playerAttackingMap = spriteParser.ParseNonAnimatedSpriteWithDirectionXML(AttackingXMLPath, Link);
            _playerTextureMap = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Load the required sprite sheet that contains Link's sprite data
        /// </summary>
        public void LoadTextures()
        {
            _playerTextureMap.Add(Link, Texture2DManager.GetLinkSpriteSheet());
            LinkSpriteSheet = Texture2DManager.GetLinkSpriteSheet();
        }
        /// <summary>
        /// Create and return a new sprite for link moving
        /// </summary>
        /// <param name="direction">The direction in which link will be facing</param>
        /// <returns></returns>
        public ISprite GetMovingSprite(Direction direction)
        {
            Debug.Assert(movementSpriteDictionary.ContainsKey(direction), $"{direction} not found in dictionary");
            return new AnimatedSprite(movementSpriteDictionary[direction], LinkSpriteSheet, 2);
        }
        /// <summary>
        /// Create and return a new attacking sprite for link
        /// </summary>
        /// <param name="direction">The direction in which link will be facing</param>
        /// <returns></returns>
        public ISprite GetAttackingSprite(Direction direction)
        {
            Debug.Assert(attackSpriteDictionary.ContainsKey(direction), $"{direction} not found in dictionary");
            return new NonAnimatedSprite(attackSpriteDictionary[direction], LinkSpriteSheet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public ISprite GetPlayerMovementSprite(string characterName, Direction direction)
        {
            Debug.Assert(_playerMovementMap.ContainsKey((characterName, direction)), $"Combined key {(characterName)},{direction} not found in dictionary");
            var key = (characterName, direction);
            List<Rectangle> spriteRectangles = _playerMovementMap[key];
            Texture2D characterTextureMap = _playerTextureMap[characterName];
            return new AnimatedSprite(spriteRectangles, characterTextureMap, AnimatedSpriteFrames);
        }

        public ISprite GetPlayerAttackingSprite(string characterName, Direction direction)
        {
            Debug.Assert(_playerAttackingMap.ContainsKey((characterName, direction)), $"Combined key {(characterName)},{direction} not found in dictionary");
            var key = (characterName, direction);
            Rectangle sprite = _playerAttackingMap[key];
            Texture2D characterTextureMap = _playerTextureMap[characterName];
            return new NonAnimatedSprite(sprite, characterTextureMap);
        }
    }
}

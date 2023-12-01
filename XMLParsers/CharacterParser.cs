using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    internal class CharacterParser
    {
        private const string HealthElement = "health";
        private const string PositionElement = "position";
        private const string DirectionElement = "direction";
        private const string HPAttribute = "hp";
        private const string DirAttribute = "dir";
        private const string RootName = "Character";
        private readonly XDocTools _docTools;

        /// <summary>
        /// Constructs an instance of a new character parser object that can parse a character.xml file 
        /// </summary>
        public CharacterParser()
        {
            _docTools = new XDocTools();
        }


        /// <summary>
        /// Parses a single character's information from the document
        /// </summary>
        /// <param name="characterName"></param>
        /// <returns>a new instance of an IEntyt playerEntity</returns>
        public IEntity ParsePlayerCharacter(XDocument documentToParse, string characterName)
        {
            /* get root and verify it is correct */
            XElement documentRoot = documentToParse.Root;
            _docTools.CheckIfElementNull(documentRoot, RootName);
            /* get the specific character child element */
            XElement characterElement = documentRoot.Element(characterName);
            _docTools.CheckIfElementNull(characterElement, characterName);
            /* parse the information required to create a new player entity */
            Vector2 playerPosition = _docTools.ParseVector2Element(characterElement.Element(PositionElement));
            int playerHealth = _docTools.ParseAttributeAsInt(characterElement.Element(HealthElement), HPAttribute);
            Direction playerDirection = _docTools.ParseAttributeAsDirection(characterElement.Element(DirectionElement), DirAttribute);

            return new PlayerEntity(playerPosition, characterName, playerHealth, playerDirection);
        }
    }
}

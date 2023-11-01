using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    /// <summary>
    /// Contains the functions for parsing XML Files that factories will need
    /// @author Aaron Heishman
    /// </summary>
    internal class SpriteXMLParser
    {
        private const string SpritesRoot = "Sprites";
        private const string SpriteElement = "Sprite";
        private const string RectangleElement = "Rectangle";
        private const string SpriteNameAttribute = "name";
        private const string SpriteDirectionAttribute = "direction";

        /* ----------------------------- Private Functions (Might throw these in another file) ----------------------------- */

        /// <summary>
        /// A helper object to parse different types of XML files that contain sprites
        /// </summary>
        public SpriteXMLParser()
        { /* does not need anything passed in */ }

        /// <summary>
        /// Returns the proper enum direction from the given sprite element's attribute "direction"
        /// </summary>
        /// <param name="spriteElement">The xml sprite element that contains the direction attribute</param>
        /// <param name="filePath">The path of the xml file being parsed</param>
        /// <returns>The direction attribute as a Direction enum</returns>
        /// <exception cref="Exception">Throws exception if attribute is missing or null</exception>
        private Direction GetDirectionAttributeAsEnum(XElement spriteElement, string filePath)
        {
            // (Direction)Enum.Parse(typeof(Direction), spriteElement.Attribute(DIRECTION).Value)
            CheckIfNull(spriteElement, filePath, SpriteElement);
            XAttribute directionAttribute = spriteElement.Attribute(SpriteDirectionAttribute);
            if (directionAttribute == null || string.IsNullOrEmpty(directionAttribute.Value))
            {
                throw new Exception($"File {filePath} is missing attribute '{SpriteDirectionAttribute}' or attribute name is null");
            }
            return (Direction)Enum.Parse(typeof(Direction), directionAttribute.Value);
        }
        /// <summary>
        /// Gets the sprite name from the XElement and returns it as a string
        /// </summary>
        /// <param name="spriteElement"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetNameAttributeAsString(XElement spriteElement, string filePath)
        {
            /* Verify that the attribute "name" exists in the sprite element and contains a value */
            XAttribute nameAttribute = spriteElement.Attribute(SpriteNameAttribute);
            if (nameAttribute == null || string.IsNullOrEmpty(nameAttribute.Value))
            {
                throw new Exception($"File {filePath} is missing attribute '{SpriteNameAttribute}' or attribute name is null");
            }
            return nameAttribute.Value;
        }

        /// <summary>
        /// Uses CreateRectangleFromElement to create a list of Rectangles for animated sprites
        /// </summary>
        /// <param name="rectangleElements">An iterable list that should contain multiple rectangle elements</param>
        /// <returns>A list of Rectangles containing the dimensions for the sprites animation</returns>
        /// <param name="filePath">The filepath of the file being parsed</param>
        private List<Rectangle> CreateRectangleList(IEnumerable<XElement> rectangleElements, string filePath)
        {
            /* Since Rectangle elements is an enumerable object we can use Select and lambda again to create the rectangle list */
            return rectangleElements.Select(
                rectangleElement => CreateRectangle(rectangleElement, filePath)).ToList();
        }

        /// <summary>
        /// Create a singular rectangle from the given xml Element
        /// </summary>
        /// <param name="rectangleElement">the specific rectangle element containing the attributes of the rectangle element</param>
        /// <param name="filePath">the file path of the file being parsed</param>
        /// <returns></returns>
        private Rectangle CreateRectangle(XElement rectangleElement, string filePath)
        {
            CheckIfNull(rectangleElement, filePath, RectangleElement);
            int x = ParseIntAttribute(rectangleElement, "x", filePath);
            int y = ParseIntAttribute(rectangleElement, "y", filePath);
            int width = ParseIntAttribute(rectangleElement, "width", filePath);
            int height = ParseIntAttribute(rectangleElement, "height", filePath);
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Try to parse an attribute of type int
        /// </summary>
        /// <param name="element">The element containing the int attribute</param>
        /// <param name="attributeName">the name of the attribute</param>
        /// <param name="fileName">the file path of the xml being parsed</param>
        /// <returns>the integer value of the attribute</returns>
        /// <exception cref="Exception">Throws exception if the attribute cannot be parsed as an int</exception>
        private int ParseIntAttribute(XElement element, string attributeName, string fileName)
        {
            XAttribute attribute = element.Attribute(attributeName);
            /* check to make sure attribute is not null */
            if (attribute == null)
            {
                throw new Exception($"Error in File {fileName}: '{RectangleElement}' element missing '{attributeName}'.");
            }
            // try to parse the attribute. if successful then returns the result, else throws an exception
            if (!int.TryParse(attribute.Value, out int result))
            {
                throw new Exception($"Invalid integer attribute found in file '{fileName} for '{attributeName}'.");
            }
            return result;
        }

        /// <summary>
        /// Checks if the XMLElements enumerable is null, then checks each element and verifies they match
        /// the name given in expectedElementsName
        /// </summary>
        /// <param name="xmlElement">The specific element being checked</param>
        /// <param name="filePath">the file path of the xml file being parsed</param>
        /// <param name="expectedElementName">The expected name of the element being checked</param>
        /// <exception cref="Exception">Throws an exception if there are is no element</exception>
        private void CheckIfNull(XElement xmlElement, string filePath, string expectedElementName)
        {
            if (xmlElement == null || xmlElement.Name != expectedElementName)
            {
                throw new Exception($"Error parsing {filePath}: Missing Element");
            }
        }

        /* ----------------------------- Public functions ----------------------------- */

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="filePath">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        public Dictionary<string, List<Rectangle>> ParseAnimatedSpriteXML(string filePath)
        {
            XDocument spriteXML = XDocument.Load(filePath);
            XElement root = spriteXML.Root;
            CheckIfNull(root, filePath, SpritesRoot);
            IEnumerable<XElement> rootElements = root.Elements(SpriteElement);
            return rootElements.ToDictionary(
                    spriteElement => GetNameAttributeAsString(spriteElement, filePath),
                    spriteElement => CreateRectangleList(spriteElement.Elements(RectangleElement), filePath)
                );
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="filePath">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        public Dictionary<string, Rectangle> ParseNonAnimatedSpriteXML(string filePath)
        {
            XDocument spriteXML = XDocument.Load(filePath);
            XElement root = spriteXML.Root;
            CheckIfNull(root, filePath, SpritesRoot);
            return root.Elements(SpriteElement).ToDictionary(
                   spriteElement => GetNameAttributeAsString(spriteElement, filePath),
                   spriteElement => CreateRectangle(spriteElement.Element(RectangleElement), filePath)
            );
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing thesprite information</returns>
        /// <param name="filePath">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        public Dictionary<Direction, List<Rectangle>> ParseAnimatedSpriteWithDirectionXML(string filePath)
        {
            XDocument spriteXML = XDocument.Load(filePath);
            XElement root = spriteXML.Root;
            CheckIfNull(root, filePath, SpritesRoot);
            return root.Elements(SpriteElement).ToDictionary(
                    spriteElement => GetDirectionAttributeAsEnum(spriteElement, filePath),
                    spriteElement => CreateRectangleList(spriteElement.Elements(RectangleElement), filePath)
           );
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="filePath">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        public Dictionary<Direction, Rectangle> ParseNonAnimatedSpriteWithDirectionXML(string filePath)
        {
            XDocument spriteXML = XDocument.Load(filePath);
            XElement root = spriteXML.Root;
            CheckIfNull(root, filePath, SpritesRoot);
            return root.Elements(SpriteElement).ToDictionary(
                    spriteElement => GetDirectionAttributeAsEnum(spriteElement, filePath),
                    spriteElement => CreateRectangle(spriteElement.Element(RectangleElement), filePath)
                 );
        }
    }
}

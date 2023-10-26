using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    /// <summary>
    /// Contains the functions for parsing XML Files that factories will need
    /// </summary>
    internal static class FactoryXMLParser
    {
        /// <summary>
        /// Parses the given xml file located at {@param xmlPath} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="xml_path">correspond path to the xml file to be be parsed
        /// Must not be null. Must be an XML file. Must follow formatting</param>
        /// <exception cref="ArgumentNullException">Thrown when xml_path is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when xmlPath does not point to a valid XML file</exception>
        public static Dictionary<String, List<Rectangle>> ParseAnimatedSpriteXML(string xmlPath)
        {
            Dictionary<string, List<Rectangle>> spriteDictionary = null;  // try catch will prevent a null dictionary from being passed back
            /* Try to read the document file and parse for desired info */
            try
            {
                XDocument enemyDoc = XDocument.Load(xmlPath);
                // Using Lambda => to search through the document to get all the required information
                spriteDictionary = enemyDoc.Root.Elements("Sprite").ToDictionary(
                        // Get all "names" and create keys
                        spriteElement => spriteElement.Attribute("name").Value,
                        // Get all frame data and create a list of rectangles 
                        spriteElement => spriteElement.Elements("Rectangle")
                            .Select(rectangleElement => new Rectangle(
                                int.Parse(rectangleElement.Attribute("x").Value),
                                int.Parse(rectangleElement.Attribute("y").Value),
                                int.Parse(rectangleElement.Attribute("width").Value),
                                int.Parse(rectangleElement.Attribute("height").Value)
                    )).ToList()
               );
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(xmlPath));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {xmlPath}", xmlPath);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param xmlPath} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="xml_path">correspond path to the xml file to be be parsed
        /// Must not be null. Must be an XML file. Must follow formatting</param>
        /// <exception cref="ArgumentNullException">Thrown when xml_path is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when xmlPath does not point to a valid XML file</exception>
        public static Dictionary<String, Rectangle> ParseNonAnimatedSpriteXML(string xmlPath)
        {
            Dictionary<string, Rectangle> spriteDictionary = null; // try catch will prevent a null dictionary from being passed back
            try
            {
                XDocument enemyDoc = XDocument.Load(xmlPath);
                // Using Lambda => to search through the document to get all the required information
                spriteDictionary = enemyDoc.Root.Elements("Sprite").ToDictionary(
                        // Get all "names" and create keys
                        spriteElement => spriteElement.Attribute("name").Value,
                        // parse a single rectangle element from the document
                        spriteElement =>
                        {
                            var rectangleElement = spriteElement.Element("Rectangle");
                            return new Rectangle(
                                int.Parse(rectangleElement.Attribute("x").Value),
                                int.Parse(rectangleElement.Attribute("y").Value),
                                int.Parse(rectangleElement.Attribute("width").Value),
                                int.Parse(rectangleElement.Attribute("height").Value));
                        });
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(xmlPath));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {xmlPath}", xmlPath);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param xmlPath} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing thesprite information</returns>
        /// <param name="xml_path">correspond path to the xml file to be be parsed
        /// Must not be null. Must be an XML file. Must follow formatting</param>
        /// <exception cref="ArgumentNullException">Thrown when xml_path is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when xmlPath does not point to a valid XML file</exception>
        public static Dictionary<Direction, List<Rectangle>> ParseAnimatedSpriteWithDirectionXML(string xmlPath)
        {
            Dictionary<Direction, List<Rectangle>> spriteDictionary = null;  // try catch will prevent a null dictionary from being passed back
            /* Try to read the document file and parse for desired info */
            try
            {
                XDocument enemyDoc = XDocument.Load(xmlPath);
                // Using Lambda => to search through the document to get all the required information
                spriteDictionary = enemyDoc.Root.Elements("Sprite").ToDictionary(
                        // Get the list of directions as a key
                        spriteElement => (Direction)Enum.Parse(typeof(Direction), spriteElement.Attribute("direction").Value),
                        // Get all frame data and create a list of rectangles 
                        spriteElement => spriteElement.Elements("Rectangle")
                            .Select(rectangleElement => new Rectangle(
                                int.Parse(rectangleElement.Attribute("x").Value),
                                int.Parse(rectangleElement.Attribute("y").Value),
                                int.Parse(rectangleElement.Attribute("width").Value),
                                int.Parse(rectangleElement.Attribute("height").Value)
                    )).ToList()
               );
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(xmlPath));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {xmlPath}", xmlPath);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param xmlPath} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="xml_path">correspond path to the xml file to be be parsed
        /// Must not be null. Must be an XML file. Must follow formatting</param>
        /// <exception cref="ArgumentNullException">Thrown when xml_path is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when xmlPath does not point to a valid XML file</exception>
        public static Dictionary<Direction, Rectangle> ParseNonAnimatedSpriteWithDirectionXML(string xmlPath)
        {
            Dictionary<Direction, Rectangle> spriteDictionary = null; // try catch will prevent a null dictionary from being passed back
            try
            {
                XDocument enemyDoc = XDocument.Load(xmlPath);
                // Using Lambda => to search through the document to get all the required information
                spriteDictionary = enemyDoc.Root.Elements("Sprite").ToDictionary(
                        // Get all "names" and create keys
                        spriteElement => (Direction)Enum.Parse(typeof(Direction), spriteElement.Attribute("direction").Value),
                        // parse a single rectangle element from the document
                        spriteElement =>
                        {
                            var rectangleElement = spriteElement.Element("Rectangle");
                            return new Rectangle(
                                int.Parse(rectangleElement.Attribute("x").Value),
                                int.Parse(rectangleElement.Attribute("y").Value),
                                int.Parse(rectangleElement.Attribute("width").Value),
                                int.Parse(rectangleElement.Attribute("height").Value));
                        });
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(xmlPath));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {xmlPath}", xmlPath);
            }
            return spriteDictionary;
        }
    }
}

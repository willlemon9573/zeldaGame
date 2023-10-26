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
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="fileName">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName does not point to a valid XML file</exception>
        public static Dictionary<string, List<Rectangle>> ParseAnimatedSpriteXML(string fileName)
        {
            Dictionary<string, List<Rectangle>> spriteDictionary = null;  // try catch will prevent a null dictionary from being passed back
            /* Try to read the document file and parse for desired info */
            try
            {
                XDocument enemyDoc = XDocument.Load(fileName);
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
                throw new ArgumentNullException(nameof(fileName));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {fileName}", fileName);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="fileName">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName does not point to a valid XML file</exception>
        public static Dictionary<string, Rectangle> ParseNonAnimatedSpriteXML(string fileName)
        {
            Dictionary<string, Rectangle> spriteDictionary = null; // try catch will prevent a null dictionary from being passed back
            try
            {
                XDocument enemyDoc = XDocument.Load(fileName);
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
                throw new ArgumentNullException(nameof(fileName));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {fileName}", fileName);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of animated sprites
        /// </summary>
        /// <returns>A new dictionary containing thesprite information</returns>
        /// <param name="fileName">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName does not point to a valid XML file</exception>
        public static Dictionary<Direction, List<Rectangle>> ParseAnimatedSpriteWithDirectionXML(string fileName)
        {
            Dictionary<Direction, List<Rectangle>> spriteDictionary = null;  // try catch will prevent a null dictionary from being passed back
            /* Try to read the document file and parse for desired info */
            try
            {
                XDocument enemyDoc = XDocument.Load(fileName);
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
                throw new ArgumentNullException(nameof(fileName));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {fileName}", fileName);
            }
            return spriteDictionary;
        }

        /// <summary>
        /// Parses the given xml file located at {@param fileName} and returns a dictionary map of information on non animated sprites.
        /// </summary>
        /// <returns>A new dictionary containing the sprite information</returns>
        /// <param name="fileName">The file name of the XML file to be parsed. Must not be null.
        /// Must be an existing and valid xml file</param>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName does not point to a valid XML file</exception>
        public static Dictionary<Direction, Rectangle> ParseNonAnimatedSpriteWithDirectionXML(string fileName)
        {
            Dictionary<Direction, Rectangle> spriteDictionary = null; // try catch will prevent a null dictionary from being passed back
            try
            {
                XDocument enemyDoc = XDocument.Load(fileName);
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
                throw new ArgumentNullException(nameof(fileName));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"ERROR: FILE NOT FOUND: {fileName}", fileName);
            }
            return spriteDictionary;
        }
    }
}

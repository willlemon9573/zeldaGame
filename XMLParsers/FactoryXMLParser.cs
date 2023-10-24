using Microsoft.Xna.Framework;
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
        /// Parses the given xml file located at {@param xmlPath} and returns a dictionary with that information
        /// </summary>
        /// <returns>A new dictionary containing the enemy sprite information</returns>
        /// <param name="xml_path">Must not be null. Must be an XML file. Must follow formatting</param>
        /// <exception cref="ArgumentNullException">Thrown when xml_path is null</exception>
        /// <exception cref="FileNotFoundException">Thrown when xmlPath does not point to a valid XML file</exception>
        public static Dictionary<String, List<Rectangle>> ParseDungeonEnemyFile(string xmlPath)
        {
            Dictionary<string, List<Rectangle>> spriteDictionary = null;
            /* Try to read the document file and parse for desired info */
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDir, xmlPath);
                XDocument enemyDoc = XDocument.Load(filePath);
                // Using Lambda => to search through the document to get all the required information
                spriteDictionary = enemyDoc.Root.Elements("Sprite").ToDictionary(
                        // Key
                        spriteElement => spriteElement.Attribute("name").Value,
                        // Value
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
    }
}

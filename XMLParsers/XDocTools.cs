using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    /// <summary>
    /// Container for methods to parse files that only need to be loaded at the start of the game
    /// @author Aaron Heishman
    /// </summary>
    internal class XDocTools
    {
        /* ----------------------------- null checks ----------------------------- */

        /// <summary>
        /// Check if an atribute is null
        /// </summary>
        /// <param name="attribute"></param>
        /// <exception cref="Exception"></exception>
        public void CheckAttribute(XAttribute attribute)
        {
            if (string.IsNullOrEmpty(attribute.Value))
            {
                throw new Exception($"Inventory XML file missing key attribute direction or direction value is null");
            }
        }

        /// <summary>
        /// Checks if the XMLElements enumerable is null, then checks each element and verifies they match
        /// the name given in expectedElementsName
        /// </summary>
        /// <param name="xmlElement">The specific element being checked</param>
        /// <param name="filePath">the file path of the xml file being parsed</param>
        /// <param name="expectedElementName">The expected name of the element being checked</param>
        /// <exception cref="Exception">Throws an exception if there are is no element</exception>
        public void CheckIfElementNull(XElement xmlElement, string expectedElementName)
        {
            if (xmlElement == null || xmlElement.Name != expectedElementName)
            {
                throw new Exception($"Error parsing file: Missing Element {expectedElementName} or element is null");
            }
        }

        /* ----------------------------- Attribute Parsing ----------------------------- */

        /// <summary>
        /// Not exactly necessary....but...Parses attribute as a string
        /// </summary>
        /// <param name="attribute">The attribute to be parsed</param>
        /// <returns>The string form of the attribute value</returns>
        public string ParseAttributeAsString(XAttribute attribute)
        {
            CheckAttribute(attribute);
            return attribute.Value;
        }

        /// <summary>
        /// Parses Element for the value of the direction attribute
        /// </summary>
        /// <param name="keyElement">The element that contains a direction attribute</param>
        /// <returns>The content of the directio enum with its corresponding enum type</returns>
        public Direction ParseAttributeAsDirection(XElement keyElement, string attributeName)
        {
            XAttribute direction = keyElement.Attribute(attributeName);
            CheckAttribute(direction);
            return (Direction)Enum.Parse(typeof(Direction), direction.Value, true);
        }


        /// <summary>
        /// Parses the given element for the key attribute
        /// </summary>
        /// <param name="itemElement"></param>
        /// <returns></returns>
        public StackableItems ParseAttributeAsStackableItem(XElement itemElement, string attributeName)
        {
            XAttribute key = itemElement.Attribute(attributeName);
            CheckAttribute(key);
            return (StackableItems)Enum.Parse(typeof(StackableItems), key.Value, true);
        }

        public Rectangle CreateRectangle(XElement rectangleElement)
        {
            int x = ParseIntAttribute(rectangleElement, "x");
            int y = ParseIntAttribute(rectangleElement, "y");
            int width = ParseIntAttribute(rectangleElement, "width");
            int height = ParseIntAttribute(rectangleElement, "height");
            return new Rectangle(x, y, width, height);
        }

        public EquipmentItem ParseAttributeAsEquipmentItem(XElement element, string attributeName)
        {
            XAttribute key = element.Attribute(attributeName);
            CheckAttribute(key);
            return (EquipmentItem)Enum.Parse(typeof(EquipmentItem), key.Value, true);
        }
        /// <summary>
        /// Parses element for the value of the sprite effects
        /// </summary>
        /// <param name="keyElement">The element that contains the SpriteEffects attribute</param>
        /// <returns>The content of the sprite effects attribute as its correspond enum type</returns>
        public SpriteEffects ParseAttributeAsSpriteEffect(XElement keyElement, string attributeName)
        {
            XAttribute spriteEffect = keyElement.Attribute(attributeName);
            CheckAttribute(spriteEffect);
            return (SpriteEffects)Enum.Parse(typeof(SpriteEffects), spriteEffect.Value, true);
        }

        /// <summary>
        /// Parses an element for Non Animated Item Spritei nformation
        /// </summary>
        /// <param name="element">The element to be parsed</param>
        /// <param name="attributeName">the attribute name</param>
        /// <returns></returns>
        public ISprite ParseAttributeAsSprite(XElement element, string attributeName)
        {
            XAttribute spriteAttribute = element.Attribute(attributeName);
            CheckAttribute(spriteAttribute);
            return ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(spriteAttribute.Value);
        }

        /// <summary>
        /// Parses the element for the given attribute and returns it as an int
        /// </summary>
        /// <param name="element">the element that contains the attribute to be parsed</param>
        /// <param name="attributeName">the attribute to name to parse</param>
        /// <returns>an integer if successfull</returns>
        /// <exception cref="Exception">Throws exception if attribute cannot be parsed or is an invalid integer</exception>
        public int ParseAttributeAsInt(XElement element, string attributeName)
        {
            XAttribute intAttribute = element.Attribute(attributeName);
            CheckAttribute(intAttribute);
            if (!int.TryParse(intAttribute.Value, out int result))
            {
                throw new Exception($"Invalid integer attribute or attribute is null");
            }
            return result;
        }

        /// <summary>
        /// Parses an element for the given attribute and returns it as a keys enum
        /// </summary>
        /// <param name="element">The element to parse</param>
        /// <param name="attributeName">the name of the attribute to look for</param>
        /// <returns>A Microsoft XNA Keys Enum</returns>
        public Keys ParseAttributeAsKeys(XElement element, string attributeName)
        {
            XAttribute keys = element.Attribute(attributeName);
            CheckAttribute(keys);
            return (Keys)Enum.Parse(typeof(Keys), keys.Value);
        }

        /// <summary>
        /// Parses an element for the given attribute and returns it as a button enum
        /// </summary>
        /// <param name="element">The element to parse</param>
        /// <param name="attributeName">the name of the attribute to look for</param>
        /// <returns>A Microsoft XNA Button enum</returns>
        public Buttons ParseAttributeAsButton(XElement element, string attributeName)
        {
            XAttribute buttonAttribute = element.Attribute(attributeName);
            CheckAttribute(buttonAttribute);
            return (Buttons)Enum.Parse(typeof(Buttons), buttonAttribute.Value, true);
        }

        /// <summary>
        /// Parse the player actions commands
        /// </summary>
        /// <param name="element">The elemnt to parse</param>
        /// <param name="attributeName">the name of the attribute to look for</param>
        /// <returns>The command related to the value found in the attribute</returns>
        public ICommand ParsePlayerActionCommands(XElement element, string attributeName, string nameSpace, ICombatEntity player)
        {
            XAttribute command = element.Attribute(attributeName);
            CheckAttribute(command);
            return (ICommand)Activator.CreateInstance(Type.GetType($"{nameSpace}.{command.Value}"), player);
        }

        /// <summary>
        /// Parse the Player Menu commands (requires different values than action commands so has to be separate)
        /// </summary>
        /// <param name="element">the element to parse</param>
        /// <param name="attributeName">the attribute name the parser looks for</param>
        /// <param name="nameSpace">the namespace of the command being added (excluding the command name itself)</param>
        /// <param name="baseGameState">the name of the attribute to look for</param>
        /// <returns>The command related to the value found in the attribute</returns>
        public ICommand ParsePlayerMenuCommands(XElement element, string attributeName, string nameSpace, Game1 game)
        {
            XAttribute command = element.Attribute(attributeName);
            CheckAttribute(command);
            return (ICommand)Activator.CreateInstance(Type.GetType($"{nameSpace}.{command.Value}"), game);
        }
    }
}
